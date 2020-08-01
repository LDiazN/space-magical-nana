using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Upgradeable ship component that contains stats info
/// and upgrades info.
/// </summary>
public class UpgradeableShip : Ship
{
    /// <summary>
    /// Ship upgrades
    /// </summary>
    public ShipUpgrades upgrades;

    public int HP { get { return GetHPStat(); } }
    public int DMGL { get { return GetDamageStat(); } }
    public float RATE { get { return GetRateStat(); } }
    public float VAMP { get { return GetVampirismStat(); } }
    public float SPEED { get { return GetSpeedStat(); } }

    public int HPLevel { get { return upgrades.HPLevel; } }
    public int DMGLevel { get { return upgrades.DMGLevel; } }
    public int RATELevel { get { return upgrades.RATELevel; } }
    public int VAMPLevel { get { return upgrades.VAMPLevel; } }
    public int SPEEDLevel { get { return upgrades.SPEEDLevel; } }


    public override int GetHPStat()
    {
        return _baseStats.baseHP + UpgradesFunctions.HPGain(upgrades.HPLevel);
    }


    public override int GetDamageStat()
    {
        return _baseStats.baseDamage + UpgradesFunctions.DMGGain(upgrades.DMGLevel);
    }


    public override float GetRateStat()
    {
        return _baseStats.baseRate + UpgradesFunctions.RATEGain(upgrades.RATELevel);
    }


    public override float GetSpeedStat()
    {
        return _baseStats.baseSpeed + UpgradesFunctions.SPEEDGain(upgrades.SPEEDLevel);
    }


    public override float GetVampirismStat()
    {
        return _baseStats.baseVampirism + UpgradesFunctions.VAMPGain(upgrades.VAMPLevel);
    }


    /// <summary>
    /// Upgrades ship stat
    /// </summary>
    /// <param name="stat">Stat to upgrade</param>
    public void UpgradeStat(UpgradeableStats stat)
    {
        switch (stat)
        {
            case UpgradeableStats.HP:
                ++upgrades.HPLevel;
                break;
            case UpgradeableStats.DMG:
                ++upgrades.DMGLevel;
                break;
            case UpgradeableStats.RATE:
                ++upgrades.RATELevel;
                break;
            case UpgradeableStats.VAMP:
                ++upgrades.VAMPLevel;
                break;
            case UpgradeableStats.SPEED:
                ++upgrades.SPEEDLevel;
                break;
            default:
                throw new Exception("The fuck haces aca");
        }
    }
}
