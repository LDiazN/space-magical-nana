using System.Collections;
using UnityEngine;

/// <summary>
/// Skills with effects that last for certain time.
/// </summary>
public abstract class TimedSkill : BaseSkill
{
    /// <summary>
    /// Skill duration
    /// </summary>
    [SerializeField]
    [Min(0f)]
    protected float _duration;
    protected bool _inDuration = false;

    public float SkillDuration { get { return _duration; } }
    public bool SkillActive { get { return _inDuration; } }

    protected override bool CanStart() => !_inCD && !_inDuration;

    protected abstract void StopSkill();

    public override bool ActivateSkill()
    {
        if (!CanStart())
            return false;

        DoSkill();
        StartCoroutine(DurationRoutine());
        return true;
    }

    /// <summary>
    /// Coroutine that manages the skill duration and starts
    /// the skill cooldown routine.
    /// </summary>
    private IEnumerator DurationRoutine()
    {
        _inDuration = true;
        yield return new WaitForSeconds(_duration);
        _inDuration = false;
        StopSkill();
        StartCoroutine(DurationRoutine());
    }
}


/// <summary>
/// Skills with inmediate effects
/// </summary>
public abstract class InstantSkill : BaseSkill
{
    protected override bool CanStart() => !_inCD;

    public override bool ActivateSkill()
    {
        if (!CanStart())
            return false;

        DoSkill();
        StartCoroutine(CDRoutine());
        return true;
    }
}
