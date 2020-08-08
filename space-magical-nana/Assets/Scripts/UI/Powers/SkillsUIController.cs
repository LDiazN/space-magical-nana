using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsUIController : MonoBehaviour
{
    public SamplePlayerControllerUI playerController; //@TODO we have to change this for the actual controller


    private void Start()
    {
        playerController.ShowSkillsUI += OnShowSkillUI;
        playerController.HideSkillsUI += OnHideSkillUI;
    }

    public void OnShowSkillUI() { }
    public void OnHideSkillUI() { }
}
