using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    [SerializeField]
    private SkillMetadata metadata;

    public SkillMetadata Metadata
    {
        get
        {
            return metadata;
        }
    }


}
