using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Class dedicated to provide a way to load/save and access/store the player ships
/// for other class as the Store/Game to access.
/// </summary>
public class PlayerGarage : MonoBehaviour
{
    public static PlayerGarage Instance { get; private set; }

    private Dictionary<PlayerShips, ShipBaseStatsSO> _shipBaseStats;
    private Dictionary<PlayerShips, ShipUpgrades> _shipUpgrades;


    private void Start()
    {
        Instance = this;
    }


    public void UpgradeShip(PlayerShips ship, UpgradeableStats stat)
    {
        _shipUpgrades[ship].UpgradeStat(stat);
        SaveShipData(ship);
    }


    private void SaveShipData(PlayerShips ship)
    { 
        // Depends on the platform
    }


    private void LoadShipData(PlayerShips ship)
    {
        string name = Enum.GetName(typeof(PlayerShips), ship);

        _shipBaseStats[ship] = Resources.Load(Path.Combine("BaseStats", name)) as ShipBaseStatsSO;
        // We load here the updgrades
    }


    private void LoadShips()
    {
        _shipBaseStats = new Dictionary<PlayerShips, ShipBaseStatsSO>();
        _shipUpgrades = new Dictionary<PlayerShips, ShipUpgrades>();

        foreach (PlayerShips ship in Enum.GetValues(typeof(PlayerShips)))
            LoadShipData(ship);
    }


    public ShipBaseStatsSO GetShipStats(PlayerShips ship)
    {
        return _shipBaseStats[ship];
    }


    public ShipUpgrades GetShipUpgrades(PlayerShips ship)
    {
        return _shipUpgrades[ship];
    }
}


/// <summary>
/// Enum containing the player ship names
/// </summary>
public enum PlayerShips
{
    LaPerrona,
    LaDomadora,
    LaMamadora,
    LaCalentona,
}