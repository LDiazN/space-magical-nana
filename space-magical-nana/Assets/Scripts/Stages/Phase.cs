using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A phase is a wrapper for every class that will iterate over the 
/// gameplay, such as a stage or a wave
/// </summary>
public class Phase : MonoBehaviour
{

    /// <summary>
    /// This delegate defines the signature of a function to be called when this 
    /// phase is ended
    /// </summary>
    public delegate void OnPhaseEnded();

    /// <summary>
    /// This event will notify to other objects when the current 
    /// phase has ended
    /// </summary>
    event OnPhaseEnded PhaseEnded;

    /// <summary>
    /// Every object that is interested in react to the event ending
    /// should subscribe this event
    /// </summary>
    /// <param name="handler"> Handler function to execute when this phase is ended </param>
    public void SubscribePhase(OnPhaseEnded handler) => PhaseEnded += handler;

    /// <summary>
    /// If some object doesn't care about this phase anymore, it may use this method
    /// to stop listening the event
    /// </summary>
    /// <param name="handler"> Function to be desuscribed </param>
    public void DeSubscribe(OnPhaseEnded handler) => PhaseEnded -= handler;

    /// <summary>
    /// This function will be called when the phase has ended. The subscribers 
    /// to this phase will be notified that the phase just ended and 
    /// the phase will be deactivated.
    /// </summary>
    public void End()
    {
        PhaseEnded();           // Notify the suscribers that this phase has ended
        enabled = false;        // Disable this object
    }

    /// <summary>
    /// Set this Phase as currently running
    /// </summary>
    public void StartPhase() => enabled = true;
    
}
