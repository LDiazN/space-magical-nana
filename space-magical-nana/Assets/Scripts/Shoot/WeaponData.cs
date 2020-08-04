using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object representing the weapon metadata
/// </summary>
[CreateAssetMenu(fileName = "new weaponData", menuName = "Weapon metadata", order = 0)]
public class WeaponData : ScriptableObject
{
    /// <summary>
    /// Weapon name in the UI
    /// </summary>
    [SerializeField]
    private string weaponName;

    /// <summary>
    /// Icon to show in the ui
    /// </summary>
    [SerializeField]
    private Sprite icon;

    /// <summary>
    /// Weapon description
    /// </summary>
    [SerializeField]
    private string description;

}
