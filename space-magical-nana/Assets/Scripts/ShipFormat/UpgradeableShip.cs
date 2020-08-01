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
    [SerializeField]
    private bool _loadFromGarage;
    [SerializeField]
    private PlayerShips _garageShip;

    /// <summary>
    /// Ship upgrades
    /// </summary>
    [SerializeField]
    private ShipUpgrades _upgrades;

    public int HP { get { return GetHPStat(); } }
    public int DMGL { get { return GetDamageStat(); } }
    public float RATE { get { return GetRateStat(); } }
    public float VAMP { get { return GetVampirismStat(); } }
    public float SPEED { get { return GetSpeedStat(); } }

    public int HPLevel { get { return _upgrades.HPLevel; } }
    public int DMGLevel { get { return _upgrades.DMGLevel; } }
    public int RATELevel { get { return _upgrades.RATELevel; } }
    public int VAMPLevel { get { return _upgrades.VAMPLevel; } }
    public int SPEEDLevel { get { return _upgrades.SPEEDLevel; } }

    protected override void Start()
    {
        base.Start();
        if (_loadFromGarage)
        {
            // Llamamos al garage
        }
    }


    public override int GetHPStat()
    {
        return _baseStats.baseHP + UpgradesFunctions.HPGain(_upgrades.HPLevel);
    }


    public override int GetDamageStat()
    {
        return _baseStats.baseDamage + UpgradesFunctions.DMGGain(_upgrades.DMGLevel);
    }


    public override float GetRateStat()
    {
        return _baseStats.baseRate + UpgradesFunctions.RATEGain(_upgrades.RATELevel);
    }


    public override float GetSpeedStat()
    {
        return _baseStats.baseSpeed + UpgradesFunctions.SPEEDGain(_upgrades.SPEEDLevel);
    }


    public override float GetVampirismStat()
    {
        return _baseStats.baseVampirism + UpgradesFunctions.VAMPGain(_upgrades.VAMPLevel);
    }
}
