using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaCannon : BaseSkill
{
    protected override void DoSkill()
    {
        Debug.Log("BOOOM pea cannon");
    }

    protected override bool CanStart() => true;

    public override void Init(GameObject g) => Debug.Log("initializing");

    public override bool ActivateSkill()
    {
        DoSkill();
        return true;
    }
}
