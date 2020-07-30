using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all moving entities
/// </summary>
public abstract class BaseMovingEntity : MonoBehaviour
{
    /// <summary>
    /// Every base class should implement a speed concept, 
    /// so this method is supposed to change that speed
    /// so the entity can slow down or accelerate
    /// </summary>
    /// <param name="f"> Next Max Speed </param>
    public abstract void SetMaxSpeed(float newSpeed);

    /// <summary>
    /// Get the current MaxSpeed of the class
    /// </summary>
    public abstract float GetMaxSpeed();

    /// <summary>
    /// Get the current ship velocity
    /// </summary>
    public abstract Vector2 GetVelocity();
}
