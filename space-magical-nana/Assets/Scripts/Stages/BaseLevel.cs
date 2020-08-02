using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class provides an interface to implement a Level
/// object
/// </summary>
class BaseLevel : MonoBehaviour
{
    [SerializeField] 
    private List<Stage> stages;


    private PhaseManager<Stage> phaseManager;
    
    private void Start()
    {
        phaseManager = new PhaseManager<Stage>(stages);
        phaseManager.RunPhases();
    }

    private void Update()
    {
        
    }
}
