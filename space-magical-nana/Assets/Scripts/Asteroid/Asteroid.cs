using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AsteroidMovement), typeof(AsteroidDestruction), typeof(BaseHealthSystem))]
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(Poolable))]

public class Asteroid: MonoBehaviour
{
    private AsteroidDestruction _destruction = null;
    private AsteroidMovement _movement = null;
    private BaseHealthSystem _hpSys = null;
    private Poolable _poolable = null;

    public delegate void AsteroidDisposal(Asteroid asteroid);
    public event AsteroidDisposal OnAsteroidDisposal;

    private Camera _main;


    private void Awake()
    {
        _movement = GetComponent<AsteroidMovement>();
        _destruction = GetComponent<AsteroidDestruction>();
        _hpSys = GetComponent<BaseHealthSystem>();
        _poolable = GetComponent<Poolable>();
        _main = Camera.main;

        _hpSys.OnNoHP += (a) => DestroyAsteroid();
    }


    public void Spawn(int HP, float size, float speed, float vpCord)
    {
        gameObject.SetActive(true);
        
        Vector2 position = _main.ViewportToWorldPoint(Vector3.right * vpCord + Vector3.up);
        Vector2 dir = ((Vector2)_main.ViewportToWorldPoint(Vector2.right * Random.Range(0.1f, 0.9f)) - position);
        transform.localScale = Vector3.one * size;

        _movement.GoToAndMove(dir, position, speed);
        _destruction.ResetDestruction();
        _hpSys.UpdateMaxHP(HP, HP);

    }

    private void DestroyAsteroid()
    {
        gameObject.SetActive(false);
        OnAsteroidDisposal.Invoke(this);

        _poolable.Dispose();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        OnAsteroidDisposal.Invoke(this);

        _poolable.Dispose();
    }
}
