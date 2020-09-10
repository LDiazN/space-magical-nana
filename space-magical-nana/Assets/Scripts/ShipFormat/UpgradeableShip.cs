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
    private bool _loadFromGarage = false;
    [SerializeField]
    private PlayerShips _garageShip = PlayerShips.LaCalentona;

    /// <summary>
    /// Ship upgrades
    /// </summary>
    [SerializeField]
    private ShipUpgrades _upgrades = null;

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
            _baseStats = PlayerGarage.Instance.GetShipStats(_garageShip);
            _upgrades = PlayerGarage.Instance.GetShipUpgrades(_garageShip);
        }
    }


    public override int GetHPStat()
    {
        return _baseStats.BaseHP + UpgradesFunctions.HPGain(_upgrades.HPLevel);
    }


    public override int GetDamageStat()
    {
        return _baseStats.BaseDamage + UpgradesFunctions.DMGGain(_upgrades.DMGLevel);
    }


    public override float GetRateStat()
    {
        return _baseStats.BaseRate + UpgradesFunctions.RATEGain(_upgrades.RATELevel);
    }


    public override float GetSpeedStat()
    {
        return _baseStats.BaseSpeed + UpgradesFunctions.SPEEDGain(_upgrades.SPEEDLevel);
    }


    public override float GetVampirismStat()
    {
        return _baseStats.BaseVampirism + UpgradesFunctions.VAMPGain(_upgrades.VAMPLevel);
    }
}
