using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaGatlin : BaseSkill
{
    protected override void DoSkill()
    {
        Debug.Log("Traatatatatatata pea gatlin");
    }

    protected override bool CanStart() => true;

    public override void Init(GameObject g) => Debug.Log("initializing");

    public override bool ActivateSkill()
    {
        DoSkill();
        return true;
    }


}
