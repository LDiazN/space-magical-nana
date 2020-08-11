using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUIController : MonoBehaviour
{
    /// <summary>
    /// The player controller, so the ui can get info about the player
    /// </summary>
    public SamplePlayerControllerUI playerController; //@TODO we have to change this for the actual controller

    /// <summary>
    /// List of skills the player has
    /// </summary>
    [SerializeField] 
    private List<BaseSkill> skills;

    /// <summary>
    /// The button object to spawn for each stored skill.
    /// It should have attached a button component and stored in a prefab
    /// </summary>
    public GameObject button;

    /// <summary>
    /// buttons spawned by the script
    /// </summary>
    private List<Button> buttons;

    /// <summary>
    /// offset in degrees between each button
    /// </summary>
    [SerializeField]
    private float buttonsOffset = 10f;

    public float ButtonOffset
    {
        set { buttonsOffset = Mathf.Clamp(value, 0, 360); }

        get
        {
            return buttonsOffset;
        }
    }

    /// <summary>
    /// distance from the player for the buttons to spawn
    /// </summary>
    [SerializeField]
    private float radius = 2f;

    /// <summary>
    /// Position for each button
    /// </summary>
    private List<Vector2> btnPositions;

    private void Start()
    {
        // clamp the offset degrees so it's contained in 360 degrees
        if (skills.Count > 0)
            buttonsOffset = Mathf.Clamp(buttonsOffset, 0, 360);

        // The only thing we do with player controller: subscribe to its signals
        playerController.ShowSkillsUI += OnShowSkillUI;
        playerController.HideSkillsUI += OnHideSkillUI;

        // Check if the button contains a button component
        if (button.GetComponent<Button>() == null)
        {
            throw new System.ArgumentException(
                    "Unvalid button template for SkillsUIController: " +
                    "The given game object is not a button object. Please " +
                    "add a game object with a button component attached to it."
                );
        }

        // create the buttons, initialize them and store them
        buttons = new List<Button>(skills.Count);
        foreach (BaseSkill skill in skills)
        {
            var b = Instantiate(button,transform);          // Create the button
            var buttonComp = b.GetComponent<Button>();      // Get the button component

            buttonComp.image.sprite = skill.MetaData.Icon;  // set the button background to the skill icon
            b.SetActive(false);                             // Deactivate the button
            b.transform.position = Vector3.zero;

            buttons.Add(buttonComp);                        //Store the button
        }

        btnPositions = new List<Vector2>(skills.Count);

        foreach (BaseSkill _ in skills)
            btnPositions.Add(Vector2.zero);

        // Set the actual position for each button
        ComputePositions();
        UpdateBtnPositions();
    }

    /// <summary>
    /// Start the process of showing the skills
    /// </summary>
    private void OnShowSkillUI()
    {
        ShowSkills();
    }

    /// <summary>
    /// Start the process of hiding the skills
    /// </summary>
    private void OnHideSkillUI()
    {
        HideSkills();
    }

    /// <summary>
    /// Set every skill as active in order to make them visible
    /// </summary>
    private void ShowSkills () =>
        buttons.ForEach(b => b.gameObject.SetActive(true));
    
    /// <summary>
    /// Set every skill as unactive so they become invisible
    /// </summary>
    private void HideSkills()  => 
        buttons.ForEach(b => b.gameObject.SetActive(false));
    
    /// <summary>
    /// Compute the position where each button should be placed 
    /// </summary>
    private void ComputePositions()
    {
        if (skills.Count == 0)
            return;

        // Compute the initial position 
        Vector2 from = Vector2.down;
        var angle = buttonsOffset * (Mathf.PI / 180);
        float init;
        if (skills.Count % 2 == 0)
            init = 3f * Mathf.PI / 2f - (skills.Count - 1) / 2 * angle - angle / 2 ;
        else
            init = 3f * Mathf.PI / 2f - skills.Count / 2 * angle;


        // Store the positions
        for (int i = 0; i < skills.Count; i++)
            btnPositions[i] = radius *  new Vector2(
                                    Mathf.Cos(init + i * angle),
                                    Mathf.Sin(init + i * angle)
                                );

    }

    // Update the corresponding position to each button
    private void UpdateBtnPositions()
    {
        for (int i = 0; i < skills.Count; i++)
            buttons[i].transform.position = btnPositions[i];
    }
    
}
