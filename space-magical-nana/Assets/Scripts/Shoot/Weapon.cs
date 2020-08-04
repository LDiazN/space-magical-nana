using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class to inherit weapons
/// </summary>
public class Weapon : MonoBehaviour
{
    /// <summary>
    /// Weapon metadata, such as name, description, icon
    /// </summary>
    [SerializeField]
    protected WeaponData metadata;

    /// <summary>
    /// Object pooler for this weapon bullets
    /// </summary>
    [SerializeField]
    protected ObjectPool bullets;

    /// <summary>
    /// Position from the player where the bullet should spawn
    /// </summary>
    [SerializeField]
    protected Vector2 offset;

    /// <summary>
    /// The shooting function
    /// </summary>
    public virtual void Shoot()
    {
        var bullet = bullets.Get();
        bullet.transform.position = transform.position + (Vector3) offset;
    }


}
