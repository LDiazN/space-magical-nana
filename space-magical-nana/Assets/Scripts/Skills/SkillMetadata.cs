using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new skillData", menuName = "Skill Data", order = 1)]
public class SkillMetadata : ScriptableObject
{
    [SerializeField]
    private string _description;

    [SerializeField]
    private string _skillName;


    [SerializeField]
    private Sprite _icon;
    public string SkillName { get { return SkillName; } }

    public string Description { get { return _description; } }
    public string Name { get { return _skillName; } }
    public Sprite Icon { get { return _icon; } }
}
