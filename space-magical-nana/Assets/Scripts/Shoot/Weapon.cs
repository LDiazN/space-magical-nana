using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class to inherit weapons
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected Ship _ship;

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
    protected Transform[] _spawnPos;
    private int _currentGun = 0;
    [SerializeField]
    private bool _simultaneous;

    [SerializeField]
    protected int _damage;
    [SerializeField]
    [SingleLayer]
    private LayerMask _bulletLayer;


    [SerializeField]
    protected float _fireRate = 0.25f;
    protected bool _inCD = false;
    protected bool _shoot = false;
    protected Coroutine _rateRoutine = null;


    private void Awake()
    {
        _ship = GetComponentInParent<Ship>();
    }


    private void Start()
    {
        _damage = _ship.GetDamageStat();
        _fireRate = _ship.GetRateStat();
        _rateRoutine = StartCoroutine(FireRateCD());

        if (_spawnPos == null || _spawnPos.Length == 0)
            throw new System.ArgumentException(
                "Unvalid spawn positions. Spawn positions should not be null or zero length"
                );
    }


    private IEnumerator FireRateCD()
    {
        while (true)
        {
            _inCD = true;
            yield return new WaitForSeconds(_fireRate);
            _inCD = false;
            yield return new WaitUntil(() => _shoot);
            _shoot = false;
        }
    }


    /// <summary>
    /// The shooting function
    /// </summary>
    public void Shoot()
    {
        if (_inCD)
            return;

        if (!_simultaneous)
        {
            ShootAGun(_currentGun);
            _currentGun = (_currentGun + 1) % _spawnPos.Length;
        }
        else
            ShootAllGuns();

        _shoot = true;
    }


    protected virtual void ShootAGun(int gun)
    {
        Transform gunTrans = _spawnPos[_currentGun];
        GameObject bullet = bullets.Get();
        
        bullet.GetComponent<Bullet>().SpawnBullet(
            gunTrans.position, 
            gunTrans.up, 
            _damage, 
            (int) Mathf.Log(_bulletLayer,2)
            , transform
        );
    }


    private void ShootAllGuns()
    {
        for (_currentGun = 0; _currentGun < _spawnPos.Length; ++_currentGun)
            ShootAGun(_currentGun);
    }
}
