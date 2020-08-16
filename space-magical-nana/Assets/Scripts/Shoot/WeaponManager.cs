using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This is an interface to the controller for weapons
/// </summary>
public class WeaponManager : MonoBehaviour
{
    /// <summary>
    /// List of weapons holded by this object
    /// </summary>
    [SerializeField]
    private List<Weapon> _weapons;

    /// <summary>
    /// Current weapon available to shoot
    /// </summary>
    private Weapon currentWeapon;

    /// <summary>
    /// Number of available weapons
    /// </summary>
    public int nWeapons => _weapons.Count;

    private void Start()
    {
        currentWeapon = _weapons.Count > 0 ? _weapons[0] : null; // Init the current weapon
    }

    /// <summary>
    /// Interface function to call the the shoot method for the 
    /// currently selected weapon. If there's no current weapon, 
    /// shoot wont do anything
    /// </summary>
    public void Shoot() => currentWeapon?.Shoot();

    /// <summary>
    /// Add a weapon to the available weapons
    /// </summary>
    /// <param name="weapon"> Weapon to delete </param>
    public void AddWeapon(Weapon weapon) => _weapons.Add(weapon); 

    /// <summary>
    /// Remove the weapon from the available weapons
    /// </summary>
    /// <param name="weapon"> The weapon to remove </param>
    public void RemoveWeapon(Weapon weapon) => _weapons.Remove(weapon);

    /// <summary>
    /// Remove the weapon from the available weapons
    /// </summary>
    /// <param name="weaponIndex"> The index of the weapon to be deleted </param>
    public void RemoveWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= _weapons.Count)
            return;
        
        _weapons.RemoveAt(weaponIndex);
    }

    /// <summary>
    /// Change the currently shooting weapon to the given weapon, even if 
    /// this is not added to the weapon list
    /// </summary>
    /// <param name="weapon"> weapon to set as current weapon </param>
    public void SwapWeapon(Weapon weapon) => currentWeapon = weapon;

    /// <summary>
    /// Change the currently shooting weapon to the one in the given index position.
    /// Is an error to give an index out of the range of the weapon list
    /// </summary>
    /// <param name="weaponIndex"> Index of the weapon to change </param>
    public void SwapWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= _weapons.Count )
            throw new NullReferenceException("Asking for a weapon out of the weapon array");
        
        currentWeapon = _weapons[weaponIndex];
    }

}
