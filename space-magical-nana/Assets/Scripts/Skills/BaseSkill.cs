using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    /// <summary>
    /// Skill metadata
    /// </summary>
    [SerializeField]
    private SkillMetadata _metadata;

    /// <summary>
    /// Skill cooldown
    /// </summary>
    [SerializeField]
    [Min(0.1f)]
    private float _cdTime;
    private bool _inCD = false;


    /// <summary>
    /// Skill duration
    /// </summary>
    [SerializeField]
    [Min(0f)]
    private float _duration;
    private bool _inDuration = false;


    public bool Instantaneous { get { return _duration == 0; } }
    public SkillMetadata MetaData { get { return _metadata; } }

    private bool CanStart() => !_inCD && !_inDuration;
    
    protected abstract void DoSkill();
    protected abstract void StopSkill();


    /// <summary>
    /// Activates the skill
    /// </summary>
    public void ActivateSkill()
    {
        if (!CanStart())
            return;

        DoSkill();

        if (!Instantaneous)
            StartCoroutine(DurationRoutine());
        else
            StartCoroutine(CDRoutine());
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
        StartCoroutine(CDRoutine());
    }


    /// <summary>
    /// Coroutine that manages the skill cooldown.
    /// </summary>
    private IEnumerator CDRoutine()
    {
        _inCD = true;
        yield return new WaitForSeconds(_cdTime);
        _inCD = false;
    }
}