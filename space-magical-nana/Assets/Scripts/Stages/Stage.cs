using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a test code for now, we have to change this
/// </summary>
public class Stage : Phase
{

    [SerializeField]
    protected List<Wave> _waves;

    protected PhaseManager<Wave> _waveManager;

    private void Awake()
    {
        Setup();
    }

    /// <summary>
    /// Called in the start
    /// </summary>
    protected virtual void Setup()
    {
        _waveManager = new PhaseManager<Wave>(_waves);
        SubscribePhaseStarted(_waveManager.RunPhases);
    }
}
