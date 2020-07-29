using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerativeHealth : BaseHealthSystem
{
    /// <summary>
    /// Amount to be healed per second.
    /// </summary>
    [SerializeField]
    private int _hpPerSecond = 2;
    /// <summary>
    /// Regeneration CoolDown
    /// </summary>
    [SerializeField]
    private float _regenerationCD = 5f;
    /// <summary>
    /// Indicates if the ship is regenerating.
    /// </summary>
    private bool _regenerating;
    /// <summary>
    /// Indicates if the ship took a hit.
    /// </summary>
    private bool _tookHit;


    protected override void Start()
    {
        base.Start();
        _regenerating = false;
        // Maybe we could add a delay
        StartCoroutine(RegenerationCD());
    }


    public override void TakeDamage(int damage)
    {
        _tookHit = true;
        base.TakeDamage(damage);
        if (_regenerating)
        {
            _regenerating = false;
            _tookHit = false;

            StopCoroutine(LifeRegeneration());
            StartCoroutine(RegenerationCD());
        }
    }


    /// <summary>
    /// Coroutine that handles the CoolDown of the regeneration.
    /// </summary>
    private IEnumerator RegenerationCD()
    {
        float timer = 0f;
        while (timer < _regenerationCD)
        {
            yield return null;
            if (_tookHit)
            {
                timer = 0;
                _tookHit = false;
            }
            else
                timer += Time.deltaTime;
        }
        _regenerating = true;
        StartCoroutine(LifeRegeneration());
    }


    /// <summary>
    /// Coroutine that handles the ship regeneration.
    /// </summary>
    /// <returns></returns>
    private IEnumerator LifeRegeneration()
    {
        TakeHeal(_hpPerSecond);
        while (_actualHP < _maxHP)
        {
            yield return new WaitForSeconds(1f);
            TakeHeal(_hpPerSecond);
        }
        _regenerating = false;
    }
}
