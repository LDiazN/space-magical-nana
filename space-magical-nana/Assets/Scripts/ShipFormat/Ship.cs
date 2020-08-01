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


    /// <summary>
    /// Gets ship HP
    /// </summary>
    /// <returns>Ship HP</returns>
    public virtual int GetHPStat()
    {
        return _baseStats.baseHP;
    }


    /// <summary>
    /// Gets ship damage
    /// </summary>
    /// <returns>Ship damage</returns>
    public virtual int GetDamageStat()
    {
        return _baseStats.baseDamage;
    }


    /// <summary>
    /// Gets ship fire rate
    /// </summary>
    /// <returns>Ship fire rate</returns>
    public virtual float GetRateStat()
    {
        return _baseStats.baseRate;
    }


    /// <summary>
    /// Gets ship speed
    /// </summary>
    /// <returns>Ship speed</returns>
    public virtual float GetSpeedStat()
    {
        return _baseStats.baseSpeed;
    }


    /// <summary>
    /// Gets ship vampirism
    /// </summary>
    /// <returns>Ship vampirism</returns>
    public virtual float GetVampirismStat()
    {
        return _baseStats.baseVampirism;
    }
}