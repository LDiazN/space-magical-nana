using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This manager provides an interface to 
/// run a set of phases one after the other
/// </summary>
/// <typeparam name="T"> A class that inherits the base class phase </typeparam>
public class PhaseManager<T>
    where T : Phase
{
    /// <summary>
    /// The phases being called
    /// </summary>
    private List<T> phases;

    /// <summary>
    /// Pointer in the list of phases to the next phase to run 
    /// </summary>
    private int currentPhase = 0;

    /// <summary>
    /// Create a new phase manager based on a list of phases. Don't modify this list at 
    /// runtime.
    /// </summary>
    /// <param name="phases"> 
    /// The list of phases to be called. 
    /// Whenever a phase it's called, it will be set to null  in this list
    /// </param>
    public PhaseManager(List<T> phases) 
    {
        this.phases = phases;
        foreach(Phase phase in phases)
        {
            phase.enabled = false;
            phase.SubscribePhase(RunPhases);
        }
    } 

    /// <summary>
    /// Call the sequence of phases one after the other.
    /// </summary>
    public void RunPhases()
    {
        if (currentPhase >= phases.Count)
        {
            phases = null;
            return;
        }

        phases[currentPhase].StartPhase();
        phases[currentPhase] = null;
        currentPhase ++;
    } 
}
