using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public sealed class SingleLayerAttribute : PropertyAttribute
{
    public SingleLayerAttribute() { }
}
