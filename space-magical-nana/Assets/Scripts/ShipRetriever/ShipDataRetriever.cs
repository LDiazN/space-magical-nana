using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// Interface for all the classes dedicated to store/load data stored
/// of certain player ship.
/// </summary>
public interface IShipRetriever
{
    /// <summary>
    /// Generates an instance of ShipUpgrades class stored
    /// for certain ship.
    /// </summary>
    /// <param name="ship">Ship to retrieves upgrades for</param>
    /// <returns>Ship upgrades</returns>
    ShipUpgrades RetrieveShipUpgrades(PlayerShips ship);


    /// <summary>
    /// Stores a ShipUpgrades instance for certain ship into
    /// a .json file.
    /// </summary>
    /// <param name="ship">Ship to store upgrades in a .json for</param>
    /// <param name="upgrades">Upgrades of the ship</param>
    void SaveShipUpgrades(PlayerShips ship, ShipUpgrades upgrades);
}


/// <summary>
/// Temporal ship retriever for Windows Platform
/// </summary>
public class WindowsShipRetriever : IShipRetriever
{
    private readonly string PERS_SHIP_PATH = Path.Combine(Application.persistentDataPath, "PlayerShips");
    private readonly string STR_SHIP_PATH = Path.Combine(Application.streamingAssetsPath, "PlayerShips");


    private bool CanRetrieve() => System.IO.Directory.Exists(PERS_SHIP_PATH);
    

    public ShipUpgrades RetrieveShipUpgrades(PlayerShips ship)
    {
        if (!CanRetrieve())
            StreamingToPersistent();

        // Should make this a function
        string path = Path.Combine(PERS_SHIP_PATH, Enum.GetName(typeof(PlayerShips), ship) + ".json");
        return JsonUtility.FromJson<ShipUpgrades>(path);
    }


    public void SaveShipUpgrades(PlayerShips ship, ShipUpgrades upgrades)
    {
        string path = Path.Combine(PERS_SHIP_PATH, Enum.GetName(typeof(PlayerShips), ship) + ".json");
        string json = JsonUtility.ToJson(upgrades);

        using (StreamWriter fs = new StreamWriter(path, false))
            fs.WriteLine(JsonUtility.ToJson(json));
    }


    /// <summary>
    /// Copies all the ShipUpgrades .json files from Streaming to Persistent path 
    /// </summary>
    private void StreamingToPersistent()
    {
        string[] filePaths = System.IO.Directory.GetFiles(STR_SHIP_PATH);
        System.IO.Directory.CreateDirectory(PERS_SHIP_PATH);

        int c = 0;
        foreach (string file in filePaths)
        {
            string final = Path.Combine(PERS_SHIP_PATH, c + ".json");
            if (Path.GetExtension(final) == ".meta")
                continue;
            System.IO.File.Copy(file, final);
            ++c;
        }
    }
}