using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaShotgun : BaseSkill
{

    protected override void DoSkill()
    {
        Debug.Log("Pump, chkch, pump, chkch, pea shotgun");
    }

    protected override void Init()
    {
        Debug.Log("Loading those barrels pardner, pea shotgun");
    }

    protected override void StopSkill()
    {
        Debug.Log("Oh no out of shells pea shotgun");
    }
}
