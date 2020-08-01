using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Scriptable object that store ships base stats
/// </summary>
[CreateAssetMenu(fileName = "Base_Ship", menuName = "Ships/Base Ship", order = 0)]
public class ShipBaseStatsSO : ScriptableObject
{
    public int baseHP;
    public int baseDamage;
    public float baseRate;
    public float baseSpeed;
    public float baseVampirism;
}


[System.Serializable]
public class ShipUpgrades
{
    public int HPLevel = 1;
    public int DMGLevel = 1;
    public int RATELevel = 1;
    public int VAMPLevel = 0;
    public int SPEEDLevel = 1;
    

    /// <summary>
    /// Upgrades ship stat
    /// </summary>
    /// <param name="stat">Stat to upgrade</param>
    public void UpgradeStat(UpgradeableStats stat)
    {
        switch (stat)
        {
            case UpgradeableStats.HP:
                ++HPLevel;
                break;
            case UpgradeableStats.DMG:
                ++DMGLevel;
                break;
            case UpgradeableStats.RATE:
                ++RATELevel;
                break;
            case UpgradeableStats.VAMP:
                ++VAMPLevel;
                break;
            case UpgradeableStats.SPEED:
                ++SPEEDLevel;
                break;
        }
    }
}


/// <summary>
/// Enum that indicates the Upgradeable Stats
/// </summary>
public enum UpgradeableStats
{
    HP,
    DMG,
    RATE,
    VAMP,
    SPEED,
}