﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaGatlin : BaseSkill
{
    protected override void DoSkill()
    {
        Debug.Log("Traatatatatatata pea gatlin");
    }

    protected override void Init()
    {
        Debug.Log("Spinning pea gatlin");
    }

    protected override void StopSkill()
    {
        Debug.Log("wow such overhead, much hot, very unusable");
    }
}
