using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new skillData", menuName = "Skill Data", order = 1)]
public class SkillMetadata : ScriptableObject
{
    [SerializeField]
    private string description;

    [SerializeField]
    private string skillName;

    [SerializeField]
    private Sprite icon;

}
