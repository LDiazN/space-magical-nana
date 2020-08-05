using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Ship basic component that contains stat info
/// </summary>
public class Ship : MonoBehaviour
{
    /// <summary>
    /// Ship base stats
    /// </summary>
    [SerializeField]
    protected ShipBaseStatsSO _baseStats;

    public int HP { get { return GetHPStat(); } }
    public int DMG { get { return GetDamageStat(); } }
    public float RATE { get { return GetRateStat(); } }
    public float VAMP { get { return GetVampirismStat(); } }
    public float SPEED { get { return GetSpeedStat(); } }

    protected virtual void Start()
    {
    }

    /// <summary>
    /// Gets ship HP
    /// </summary>
    /// <returns>Ship HP</returns>
    public virtual int GetHPStat()
    {
        return _baseStats.BaseHP;
    }


    /// <summary>
    /// Gets ship damage
    /// </summary>
    /// <returns>Ship damage</returns>
    public virtual int GetDamageStat()
    {
        return _baseStats.BaseDamage;
    }


    /// <summary>
    /// Gets ship fire rate
    /// </summary>
    /// <returns>Ship fire rate</returns>
    public virtual float GetRateStat()
    {
        return _baseStats.BaseRate;
    }


    /// <summary>
    /// Gets ship speed
    /// </summary>
    /// <returns>Ship speed</returns>
    public virtual float GetSpeedStat()
    {
        return _baseStats.BaseSpeed;
    }


    /// <summary>
    /// Gets ship vampirism
    /// </summary>
    /// <returns>Ship vampirism</returns>
    public virtual float GetVampirismStat()
    {
        return _baseStats.BaseVampirism;
    }
}