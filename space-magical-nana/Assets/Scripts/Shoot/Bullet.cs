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


    private Collider2D _lastShooter;
    [Min(0.1f)]
    public float _maxSeparation = 15f;


    private Coroutine _shooterCheck = null;


    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _coll2d = GetComponent<Collider2D>();
    }


    public void SpawnBullet(Vector2 pos, Vector2 dir, int damage, int layer, Collider2D shooter = null)
    {
        enabled = true;

        if (shooter != null)
        {
            if (_lastShooter != null)
                Physics2D.IgnoreCollision(_coll2d, _lastShooter, false);

            Physics2D.IgnoreCollision(_coll2d, shooter);
            _lastShooter = shooter;
        }

        _damage = damage;
        transform.position = pos;
        gameObject.layer = layer;
        _velocity = dir.normalized * _speed;
        gameObject.SetActive(true);
        _shooterCheck = StartCoroutine(DistanceCheck());
    }


    private void DestroyBullet()
    {
        if (_lastShooter != null)
        {
            Physics2D.IgnoreCollision(_coll2d, _lastShooter, false);
            _lastShooter = null;
        }

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
            if (Vector2.Distance(transform.position, _lastShooter.transform.position) >= _maxSeparation)
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
