using System.Collections;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    /// <summary>
    /// Skill metadata
    /// </summary>
    [SerializeField]
    protected SkillMetadata _metadata;

    /// <summary>
    /// Skill cooldown
    /// </summary>
    [SerializeField]
    [Min(0.1f)]
    protected float _cdTime;
    protected bool _inCD = false;

    public SkillMetadata MetaData { get { return _metadata; } }
    public float CoolDown { get { return _cdTime; } }
    public bool InCoolDown { get { return _inCD; } }

    /// <summary>
    /// Indicates if the skill can be used
    /// </summary>
    /// <returns></returns>
    protected abstract bool CanStart();

    /// <summary>
    /// Initializes anything needed by the skill to work.
    /// </summary>
    /// <param name="ship">Ship owner of the skill</param>
    public abstract void Init(GameObject ship);


    /// <summary>
    /// Does the skill if it can be activated.
    /// </summary>
    public abstract bool ActivateSkill();
    /// <summary>
    /// Does the skill effect.
    /// </summary>
    protected abstract void DoSkill();

    /// <summary>
    /// Coroutine that manages the skill cooldown.
    /// </summary>
    protected IEnumerator CDRoutine()
    {
        _inCD = true;
        yield return new WaitForSeconds(_cdTime);
        _inCD = false;
    }
}
