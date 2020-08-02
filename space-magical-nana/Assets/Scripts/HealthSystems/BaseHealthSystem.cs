using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthSystem : MonoBehaviour
{
    /// <summary>
    /// Ship Max HP
    /// </summary>
    protected int _maxHP { get; private set; }
    /// <summary>
    /// Ship Actual HP
    /// </summary>
    protected int _actualHP { get; private set; }

    /// <summary>
    /// Indicates if the ship is invincible
    /// </summary>
    protected bool _invincible;


    public bool Invincible
    {
        get { return _invincible; }
        set { SetInvincibility(value); }
    }


    protected virtual void Start()
    {
        if (TryGetComponent(out Ship ship))
            _maxHP = ship.GetHPStat();

        _actualHP = _maxHP;
    }


    /// <summary>
    /// Process the amount to heal the ship.
    /// </summary>
    /// <param name="healed">Amount to be healed</param>
    public virtual void TakeHeal(int healed)
    {
        AddHP(healed);
    }


    /// <summary>
    /// Process the damage taken by the ship.
    /// </summary>
    /// <param name="damage">Damage taken</param>
    public virtual void TakeDamage(int damage)
    {
        if (_invincible)
            return;
        ReduceHP(damage);
    }


    /// <summary>
    /// Adds HP.
    /// </summary>
    /// <param name="amount">Amount to add</param>
    protected void AddHP(int amount)
    {
        _actualHP = Mathf.Clamp(_actualHP + amount, _actualHP, _maxHP);
    }


    /// <summary>
    /// Reduces HO
    /// </summary>
    /// <param name="amount">Amount to reduce</param>
    protected void ReduceHP(int amount)
    {
        _actualHP = Mathf.Clamp(_actualHP - amount, 0, _actualHP);
        if (_actualHP == 0)
        {
            Debug.Log("Rip in pieces");
        }
    }


    /// <summary>
    /// Sets invincibility mode.
    /// </summary>
    /// <param name="status">New invincibility status</param>
    public void SetInvincibility(bool status)
    {
        if (_invincible)
            StopCoroutine("InvincivilityRoutine");
        _invincible = status;
    }


    /// <summary>
    /// Starts timed invincibility.
    /// </summary>
    /// <param name="time">Time of the invincibility</param>
    public void StartInvincibility(float time)
    {
        if (time > 0)
        {
            if (_invincible)
                StopCoroutine("InvincivilityRoutine");
            StartCoroutine(InvincivilityRoutine(time));
        }
        else
            throw new ArgumentException("Wrong invincibility time");
    }


    /// <summary>
    /// Invincibility routine.
    /// </summary>
    /// <param name="time">Time of invincibility</param>
    private IEnumerator InvincivilityRoutine(float time)
    {
        _invincible = true;
        yield return new WaitForSeconds(time);
        _invincible = false;
    }
}
