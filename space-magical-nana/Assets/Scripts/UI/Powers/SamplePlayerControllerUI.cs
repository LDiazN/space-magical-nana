using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePlayerControllerUI : MonoBehaviour
{
    public float speed = 10f;

    public delegate void OnShowSkillsUI();

    public event OnShowSkillsUI ShowSkillsUI;

    public delegate void OnHideSkillsUI();

    public event OnHideSkillsUI HideSkillsUI;


    private void Update()
    {
        Vector2 input;

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Mouse0))
            ShowSkillsUI();
        else if (Input.GetKeyDown(KeyCode.Mouse1))
            HideSkillsUI();

        transform.Translate(input * speed * Time.fixedDeltaTime);
    }
}
