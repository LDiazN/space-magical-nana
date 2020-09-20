using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class provides an interface to implement a Level
/// object
/// </summary>
class BaseLevel : MonoBehaviour
{
    /// <summary>
    /// every single stage that this level has to play
    /// </summary>
    [SerializeField] 
    private List<Stage> _stages;


    /// <summary>
    /// This object will play each stage after the other
    /// </summary>
    private PhaseManager<Stage> _phaseManager;
    
    private void Start()
    {
        _phaseManager = new PhaseManager<Stage>(_stages);
        _phaseManager.RunPhases();
    }

}
