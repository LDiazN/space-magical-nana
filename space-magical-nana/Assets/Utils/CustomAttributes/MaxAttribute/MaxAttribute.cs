using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public sealed class MaxAttribute : PropertyAttribute
{
    public readonly float Max;

    public MaxAttribute(float max)
    {
        this.Max = max;
    }
}

[System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public sealed class MinMaxAttribute : PropertyAttribute
{
    public readonly float Min, Max;

    public MinMaxAttribute(float min, float max)
    {
        if (min > max)
            throw new System.Exception("Invalid MinMax attribute values");

        this.Min = min;
        this.Max = max;
    }
}