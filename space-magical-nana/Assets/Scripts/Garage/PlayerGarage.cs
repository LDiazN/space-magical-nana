using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class dedicated to provide a way to load/save and access/store the player ships
/// for other class as the Store/Game to access.
/// </summary>
public class PlayerGarage : MonoBehaviour
{
    private Dictionary<PlayerShips, ShipBaseStatsSO> _shipBaseStats;
    private Dictionary<PlayerShips, ShipUpgrades> _shipUpgrades;


    public void UpgradeShip(PlayerShips ship, UpgradeableStats stat)
    {
        _shipUpgrades[ship].UpgradeStat(stat);
        SaveShipData(ship);
    }


    private void SaveShipData(PlayerShips ship)
    { }


    private void LoadShipData(PlayerShips ship)
    { }


    private void LoadShips()
    { }


    public void CopyShip(PlayerShips ship, UpgradeableShip dest)
    { }
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