using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new skillData", menuName = "Skill Data", order = 1)]
public class SkillMetadata : ScriptableObject
{
    [SerializeField]
    private string description;

    public string Description { 
        get
        {
            return description;
        }
    }

    [SerializeField]
    private string skillName;

    public string SkillName
    {
        get
        {
            return SkillName;
        }
    }

    [SerializeField]
    private Sprite icon;

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }
}
