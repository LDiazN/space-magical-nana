﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthSystem : MonoBehaviour
{
    /// <summary>
    /// Ship Max HP
    /// </summary>
    [SerializeField]
    [Min(1)]
    protected int _maxHP;
    /// <summary>
    /// Ship Actual HP
    /// </summary>
    [SerializeField]
    [Min(0)]
    protected int _actualHP;

    public delegate void HPEvent(int actHP);
    public event HPEvent OnLifeChange;
    public event HPEvent OnNoHP;

    /// <summary>
    /// Indicates if the ship is invincible
    /// </summary>
    protected bool _invincible;

    public float MaxHP
    { 
        get { return _maxHP; } 
    }

    public float HP
    {
        get { return _actualHP; }
    }

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

    public void UpdateMaxHP(int max, int hp)
    {
        if (hp > max || hp < 1 || max < 1)
            throw new ArgumentException("Invalid new HP update");
        _maxHP = max;
        _actualHP = hp;
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
        if (amount == 0)
            return;
        _actualHP = Mathf.Clamp(_actualHP + amount, _actualHP, _maxHP);
        OnLifeChange.Invoke(_actualHP);
    }


    /// <summary>
    /// Reduces HO
    /// </summary>
    /// <param name="amount">Amount to reduce</param>
    protected void ReduceHP(int amount)
    {
        if (amount == 0)
            return;

        _actualHP = Mathf.Clamp(_actualHP - amount, 0, _actualHP);
        if (_actualHP == 0)
        {
            OnNoHP.Invoke(0);
        }
        OnLifeChange.Invoke(_actualHP);
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
