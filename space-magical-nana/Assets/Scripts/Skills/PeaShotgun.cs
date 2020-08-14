using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShotgun : BaseSkill
{

    protected override void DoSkill()
    {
        Debug.Log("Pump, chkch, pump, chkch, pea shotgun");
    }

    protected override bool CanStart() => true;

    public override void Init(GameObject g) => Debug.Log("initializing");

    public override bool ActivateSkill()
    {
        DoSkill();
        return true;
    }
}
