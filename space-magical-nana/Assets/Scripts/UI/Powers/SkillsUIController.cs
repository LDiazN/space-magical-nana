using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class SkillsUIController : MonoBehaviour
{
    /// <summary>
    /// The player controller, so the ui can get info about the player
    /// </summary>
    [SerializeField]
    private SamplePlayerControllerUI _playerController; //@TODO we have to change this for the actual controller

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
    private float _buttonsOffset = 10f;

    /// <summary>
    /// distance from the player for the buttons to spawn
    /// </summary>
    [SerializeField]
    [Min(0f)]
    private float _radius = 2f;

    /// <summary>
    /// How much time for the animation to end
    /// </summary>
    [SerializeField]
    [Min(0f)]
    private float _animationTime = 1f;

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

    private void Start()
    {
        // clamp the offset degrees so it's contained in 360 degrees
        if (_skills.Count > 0)
            _buttonsOffset = Mathf.Clamp(_buttonsOffset, 0, 360);

        // The only thing we do with player controller: subscribe to its signals
        _playerController.ShowSkillsUI += OnShowSkillUI;
        _playerController.HideSkillsUI += OnHideSkillUI;

        // Check if the button contains a button component
        if (_button.GetComponent<Button>() == null)
        {
            throw new System.ArgumentException(
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
        //UpdateBtnPositions();   //Set the button game object to that position

    }

    /// <summary>
    /// Start the process of showing the skills
    /// </summary>
    private void OnShowSkillUI()
    {
        StopAllCoroutines();
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

        // Compute the initial position 
        Vector2 from = Vector2.down;
        var angle = _buttonsOffset * (Mathf.PI / 180);
        float init;
        if (nSkills % 2 == 0)
            init =  3f * Mathf.PI / 2f 
                    - (nSkills - 1) / 2 * angle 
                    - angle / 2 ;
        else
            init =  3f * Mathf.PI / 2f 
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
    
    UnityAction TrySkill(Func<bool> func, Button btn)
    {
        void f()
        {
            if (!func())
                SkillFailed(btn);
        }

        return f;
    }
    
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
}
