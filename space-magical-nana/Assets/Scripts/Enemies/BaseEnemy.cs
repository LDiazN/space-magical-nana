using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(MovingEntity))]
[RequireComponent(typeof(BaseHealthSystem))]
public class BaseEnemy : MonoBehaviour
{
    /// <summary>
    /// Component to perform movement for this enemy
    /// </summary>
    protected MovingEntity _movingEntity;

    /// <summary>
    /// Component to manage the health for this enemy
    /// </summary>
    protected BaseHealthSystem _healthSystem;

    /// <summary>
    /// Event called when this enemy gets killed
    /// </summary>
    event Action<BaseEnemy> EnemyKilled;
    

    private void Awake()
    {
        // Set up components
        _movingEntity = GetComponent<MovingEntity>();
        _healthSystem = GetComponent<BaseHealthSystem>();
    }

    /// <summary>
    /// Listen to the EnemyKilled event
    /// </summary>
    /// <param name="eventHandler"></param>
    public void SubscribeEnemyKilled(Action<BaseEnemy> eventHandler) => EnemyKilled += eventHandler;

    /// <summary>
    /// Stop listening to the EnemyKilled event
    /// </summary>
    /// <param name="eventHandler"></param>
    public void DeSubscribeEnemyKilled(Action<BaseEnemy> eventHandler) => EnemyKilled -= eventHandler;

    /// <summary>
    /// Function to be called when a enemy enters to the scene
    /// </summary>
    public virtual void Spawn() { }

    /// <summary>
    /// Called when an enemy gets killed
    /// </summary>
    public virtual void Killed()
    {
        EnemyKilled(this);
    }
}
