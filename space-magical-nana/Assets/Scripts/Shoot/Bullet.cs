using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple base class to build bullets.
/// This is for testing only in the meanwhile
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Poolable))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private Collider2D _coll2d;
    private Poolable _pool;

    [SerializeField]
    [Min(0)]
    private int _damage;

    [SerializeField]
    private float _speed;
    private Vector2 _velocity;


    private Transform _lastShooter;
    [Min(0.1f)]
    public float _maxSeparation = 15f;


    private Coroutine _shooterCheck = null;


    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _coll2d = GetComponent<Collider2D>();
    }


    public void SpawnBullet(Vector2 pos, Vector2 dir, int damage, int layer, Transform shooter = null)
    {
        enabled = true;

        if (shooter != null)
        {
            _lastShooter = shooter;
            _shooterCheck = StartCoroutine(DistanceCheck());
        }
        
        _damage = damage;
        transform.position = pos;
        gameObject.layer = layer;
        _velocity = dir.normalized * _speed;
        gameObject.SetActive(true);
    }


    private void DestroyBullet()
    {
        if (_lastShooter != null)
            _lastShooter = null;

        if (_shooterCheck != null)
        {
            StopCoroutine(_shooterCheck);
            _shooterCheck = null;
        }
        _pool.Dispose();
        
        // Call animation or something
        enabled = false;
    }


    private IEnumerator DistanceCheck()
    {
        while (true)
        {
            if (Vector2.Distance(transform.position, _lastShooter.position) >= _maxSeparation)
            {
                DestroyBullet();
                break;
            }
            for (int i = 0; i < 10; ++i)
                yield return null;
        }
        yield break;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        _rb2d.MovePosition( (Vector2) transform.position + _velocity * Time.fixedDeltaTime);
    }
}
