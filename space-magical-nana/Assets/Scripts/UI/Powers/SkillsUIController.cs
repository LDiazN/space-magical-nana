using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

/// <summary>
/// This class controls how the skills UI behaves depending 
/// on the number of skills, the player position on the screen.
/// </summary>
public class SkillsUIController : MonoBehaviour
{
    /// <summary>
    /// The player controller, so the ui can get info about the player
    /// </summary>
    [SerializeField]
    private PlayerController _playerController; //@TODO we have to change this for the actual controller

    /// <summary>
    /// Just to handle alpha in the UI
    /// </summary>
    [SerializeField]
    private CanvasGroup _canvasGroup;

    /// <summary>
    /// List of skills the player has
    /// </summary>
    [SerializeField] 
    private List<BaseSkill> _skills;

    /// <summary>
    /// The button object to spawn for each stored skill.
    /// It should have attached a button component and stored in a prefab
    /// </summary>
    [SerializeField] 
    private GameObject _button;

    /// <summary>
    /// offset in degrees between each button
    /// </summary>
    [SerializeField]
    [Min(0f)]
    private float _buttonsOffset = 45f;

    /// <summary>
    /// distance from the player for the buttons to spawn
    /// </summary>
    [SerializeField]
    [Min(0f)]
    private float _radius = 100f;

    /// <summary>
    /// How much time for the animation to end
    /// </summary>
    [SerializeField]
    [Min(0f)]
    private float _animationTime = 0.02f;

    /// <summary>
    /// Player camera
    /// </summary>
    [SerializeField]
    private Camera _cam;

    /// <summary>
    /// rect extents used for the player to check margins
    /// </summary>
    [SerializeField]
    private Vector2 _playerRectsSize;

    /// <summary>
    /// How far from the player we should cast the rects
    /// </summary>
    [SerializeField]
    [Min(0f)]
    private float _playerRectsRadius;

    /// <summary>
    /// Just a handy way to store all the data for a single UI
    /// skill button together
    /// </summary>
    private struct SkillBtnData
    {
        public Vector2 position;
        public Button component;
        public BaseSkill skill;
        public SkillBtnData(Vector2 position, Button component, BaseSkill skill)
        {
            this.position = position;
            this.component = component;
            this.skill = skill;
        }
    }
    /// <summary>
    /// List of skills to handle
    /// </summary>
    private List<SkillBtnData> _skillButtons;


    /// <summary>
    /// A rect containing the world part that's entirely
    /// contained by the screen
    /// </summary>
    private Rect _worldExtents;

    /// <summary>
    /// We check if the skills will be visible by checking
    /// that all the surroinding rects are totally contained in 
    /// the world extents rect
    /// </summary>
    private Rect[] _playerRects = new Rect[4];

    private enum RectAt
    {
        Left  = 0,
        Down  = 1,
        Right = 2,
        Top   = 3
    }


    private enum UIState
    {
        Top,
        Bottom,
        Left,
        Right,
        TopRight,
        TopLeft,
        BottomRight,
        BottomLeft
    }

    private UIState _currentState = UIState.Bottom;

    // -- In game Execution ------------------
    private void Start()
    {
        // clamp the offset degrees so it's contained in 360 degrees
        if (_skills.Count > 0)
            _buttonsOffset = Mathf.Clamp(_buttonsOffset, 0, 360);

        // The only thing we do with player controller: subscribe to its signals
        _playerController.EnteringSlowMo += OnShowSkillUI;
        _playerController.EnteringFiring += OnHideSkillUI;

        // Check if the button contains a button component
        if (_button.GetComponent<Button>() == null)
        {
            throw new ArgumentException(
                    "Unvalid button template for SkillsUIController: " +
                    "The given game object is not a button object. Please " +
                    "add a game object with a button component attached to it."
                );
        }

        _skillButtons = new List<SkillBtnData>(_skills.Count);

        foreach (BaseSkill skill in _skills)
        {
            var b = Instantiate(_button,transform);          // Create the button
            var buttonComp = b.GetComponent<Button>();      // Get the button component

            buttonComp.image.sprite = skill.MetaData.Icon;  // set the button background to the skill icon
            b.SetActive(false);                             // Deactivate the button
            b.transform.position = Vector3.zero;

            buttonComp.onClick.AddListener(TrySkill(skill.ActivateSkill, buttonComp));

            // Store the button data in the button list
            _skillButtons.Add(new SkillBtnData(Vector2.zero, buttonComp, skill));
        }

        // The buttons start unactive, so they have to be invisible at the beginning
        _canvasGroup.alpha = 0f;

        // Set the actual position for each button
        ComputePositions();     //Set each button data its corresponding position

        // Get the world coordinates based on the camera and 
        // create the worldExtents rect

        var min = new Vector2(0, 0);
        var max = new Vector2(Screen.width, Screen.height);
        
        min = _cam.ScreenToWorldPoint(min);
        max = _cam.ScreenToWorldPoint(max);

        _worldExtents = new Rect(min, max - min);

        // Init the size for each detecting rect

        for(int i = 0; i < _playerRects.Length; i ++)
        {
            var temp = _playerRects[i];
            temp.size = _playerRectsSize;
            _playerRects[i] = temp;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_worldExtents.center, _worldExtents.size);
        Gizmos.color = Color.yellow;

        foreach (var r in _playerRects)
        {
            Gizmos.DrawWireCube(r.center, r.size);
            Debug.Log("Rect: " + r);
        }
        
    }

    /// <summary>
    /// Start the process of showing the skills
    /// </summary>
    private void OnShowSkillUI()
    {
        // Compute the next position for the casting rects:
        SetPlayerRectsPositions();
        // In case there's an animation in progress
        StopAllCoroutines();
        // Update where the ui should spawn
        UpdateUIState();
        // Compute the position relative to the parent where each skill should appear
        ComputePositions();
        // Animate the buttons 
        StartCoroutine(ButtonMotion(true));
    }

    /// <summary>
    /// Start the process of hiding the skills
    /// </summary>
    private void OnHideSkillUI()
    {
        StopAllCoroutines();
        StartCoroutine(ButtonMotion(false));
    }

    /// <summary>
    /// Set every skill as active in order to make them visible
    /// </summary>
    private void ShowSkills () =>
        _skillButtons.ForEach(b => b.component.gameObject.SetActive(true));


    /// <summary>
    /// Set every skill as unactive so they become invisible
    /// </summary>
    private void HideSkills()  => 
        _skillButtons.ForEach(b => b.component.gameObject.SetActive(false));
    
    /// <summary>
    /// Compute the position where each button should be placed 
    /// </summary>
    private void ComputePositions()
    {
        var nSkills = _skillButtons.Count;

        if ( nSkills == 0)
            return;

        // Compute angle offset based on the number of elements
        // and the ui state
        float angle;
        switch (_currentState)
        {
            case UIState.Bottom:
            case UIState.Top:
            case UIState.Left:
            case UIState.Right:
                angle = Mathf.PI / nSkills;
                break;
            default:
                angle = ( Mathf.PI / 2 ) / nSkills;
                break;
        }

        // Compute the initial position from the current state
        float startPos;
        var pi = Mathf.PI;
        switch (_currentState)
        {
            case UIState.TopRight:
                startPos = pi / 4.0f;
                break;
            case UIState.Top:
                startPos = pi / 2.0f;
                break;
            case UIState.TopLeft:
                startPos = 3.0f * pi / 4.0f;
                break;
            case UIState.Left:
                startPos = pi;
                break;
            case UIState.BottomLeft:
                startPos = 5.0f * pi / 4.0f;
                break;
            case UIState.Bottom:
                startPos = 3.0f * pi / 2.0f;
                break;
            case UIState.BottomRight:
                startPos = 7.0f * pi / 4.0f;
                break;
            default:
                startPos = 0.0f;
                break;
        }

        float init;
        if (nSkills % 2 == 0)
            init =  startPos 
                    - (nSkills - 1) / 2 * angle 
                    - angle / 2 ;
        else
            init =  startPos 
                    - nSkills / 2 * angle;


        // Store the positions
        for (int i = 0; i < nSkills ; i++)
        {
            var tmp = _skillButtons[i];

            tmp.position = _radius *  new Vector2(
                                        Mathf.Cos(init + i * angle),
                                        Mathf.Sin(init + i * angle)
                                        );

            _skillButtons[i] = tmp;
        }

    }

    // Update the corresponding position to each button
    private void UpdateBtnPositions() =>
        _skillButtons.ForEach( 
            btn => btn.component.transform.position = btn.position
        );
    
    /// <summary>
    /// A helpful wrapper to return a function that does a different thing
    /// depending on if it's possible to perform the skill or not
    /// </summary>
    /// <param name="func"> 
    ///     ActivateSkill function that returns true if it was
    ///     possible to do the skill 
    /// </param>
    /// <param name="btn">
    /// The Button component of the button that was touched to perform 
    /// the skill
    /// </param>
    /// <returns></returns>
    private UnityAction TrySkill(Func<bool> func, Button btn)
    {
        void f()
        {
            if (!func())
                SkillFailed(btn);
        }

        return f;
    }
    
    /// <summary>
    /// This function is called whenever the inteends to 
    /// use a skill that's not currently available
    /// </summary>
    /// <param name="btn"></param>
    private void SkillFailed(Button btn)
    {
        Debug.Log("Skill not ready to use");
    }

    // ---- Functions to animate the buttons ---- // 
    /// <summary>
    /// Animates the buttons so they fade in or fade out as specified
    /// </summary>
    /// <param name="isIn"> If the animation is fading in. Otherwise, it is fading out </param>
    /// <returns></returns>
    IEnumerator ButtonMotion(bool isIn)
    {
        if (isIn) ShowSkills();

        var initTime = _skillButtons.Count > 0 ?
                        _skillButtons[0].component.transform.position.sqrMagnitude / (_radius * _radius)
                        : 1;
        initTime *= _animationTime;

        // This variable will help to animate according to the 
        // buttons state, if they're fading in or out
        var sign = isIn ? 1 : -1;
        var t = isIn ? 0 : 1;

        for (float p = initTime; p <= _animationTime; p += Time.smoothDeltaTime)
        {
            // smoothly move each button to its corresponding position
            var p_norm = p / _animationTime;
            p_norm = Mathf.Sin(p_norm * Mathf.PI / 2);

            _canvasGroup.alpha = t + sign * p_norm;

            foreach (SkillBtnData btn in _skillButtons)
                btn
                .component
                .transform
                .localPosition = Vector2.Lerp(Vector2.zero, btn.position, t + sign * p_norm);

            yield return new WaitForSecondsRealtime(Time.smoothDeltaTime);
        }

        if (!isIn) HideSkills();
    }

    // ---- Functions to position rects ---- //

    /// <summary>
    /// Whenever the player moves, the rects position have to be updates.
    /// This function will update such positions
    /// </summary>
    private void SetPlayerRectsPositions()
    {
        var currPos = (Vector2) transform.position;
        var extents = _playerRects[0].size / 2;
        extents.x *= -1;
        extents.y *= -3/2;

        _playerRects[0].position = currPos + Vector2.left  * _playerRectsRadius + extents;
        _playerRects[1].position = currPos + Vector2.down  * _playerRectsRadius + extents;
        _playerRects[2].position = currPos + Vector2.right * _playerRectsRadius + extents;
        _playerRects[3].position = currPos + Vector2.up    * _playerRectsRadius + extents;
    }

    /// <summary>
    /// Check if the container entirely contains the 
    /// conained rect
    /// </summary>
    /// <param name="container"> Container Rect </param>
    /// <param name="contained"> Contained Rect </param>
    /// <returns></returns>
    private bool Contains(Rect container, Rect contained)
    {
        var bigMin = container.min;
        var bigMax = container.max;
        var smallMin = contained.min;
        var smallMax = contained.max;

        return bigMin.x   <= smallMin.x && bigMin.y   <= smallMin.y &&
               smallMax.x <= bigMax.x   && smallMax.y <= bigMax.y;
    }

    private void UpdateUIState()
    {
        Func<Rect, bool> InWorld = (r) => Contains(_worldExtents, r);

        var left  = _playerRects[(int)RectAt.Left ];
        var right = _playerRects[(int)RectAt.Right];
        var top   = _playerRects[ (int)RectAt.Top ];
        var down  = _playerRects[(int)RectAt.Down ];

        if (InWorld(left) && InWorld(right) && InWorld(down) && InWorld(top))
            _currentState = UIState.Bottom;
        else if (!InWorld(left) && !InWorld(down))
            _currentState = UIState.TopRight;
        else if (!InWorld(left) && !InWorld(top))
            _currentState = UIState.BottomRight;
        else if (!InWorld(top) && !InWorld(right))
            _currentState = UIState.BottomLeft;
        else if (!InWorld(down) && !InWorld(right))
            _currentState = UIState.TopLeft;
        else if (!InWorld(down))
            _currentState = UIState.Top;
        else if (!InWorld(top))
            _currentState = UIState.Bottom;
        else if (!InWorld(left))
            _currentState = UIState.Right;
        else if (!InWorld(right))
            _currentState = UIState.Left;
        else
            throw new InvalidProgramException(
                "In Updating ui position state: " + 
                "player should not be able to have more than 2 of her sides " +
                "blocked by the world limits"
                );

    }

    

}
