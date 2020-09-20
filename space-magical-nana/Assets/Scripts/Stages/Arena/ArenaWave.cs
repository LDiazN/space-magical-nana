using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWave : Wave
{

    /// <summary>
    /// Enemies of this wave
    /// </summary>
    [SerializeField]
    private List<ArenaEnemy> _enemies;


    private void Awake()
    {
        this.SubscribePhaseStarted(DeployWave);
        foreach (var enemy in _enemies)
            enemy.enabled = false;
    }

    /// <summary>
    /// Launch the wave, just enable enemies in the right order
    /// </summary>
    protected override void DeployWave ()
    {
        foreach (var enemy in _enemies)
        {
            enemy.enabled = true;
            enemy.Spawn();
        }
    }

    /// <summary>
    /// Called when a wave just ended.
    /// </summary>
    protected override void WaveEnded()
    {
        End();  // Notify that the wave just ended
    }
}
