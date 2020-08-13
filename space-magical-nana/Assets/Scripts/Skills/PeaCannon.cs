using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaCannon : BaseSkill
{
    protected override void DoSkill()
    {
        Debug.Log("BOOOM pea cannon");
    }

    protected override void Init()
    {
        Debug.Log("Charging pea Cannon");
    }

    protected override void StopSkill()
    {
        Debug.Log("Pea Cannon Out of energy for a while");
    }
}
