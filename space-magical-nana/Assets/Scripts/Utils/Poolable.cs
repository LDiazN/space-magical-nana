using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// This class provides an easy way to ensure correctness and 
/// decoupling with object pooling. Every poolable object will 
/// have an automated setup to communicate with its 
/// corresponding pool.
/// </summary>
public class Poolable : MonoBehaviour
{
    private bool _pooled;

    /// <summary>
    /// If the object is currently pooled
    /// </summary>
    public bool pooled = true;

    /// <summary>
    /// This event is called when the object should be deleted
    /// </summary>
    event Action<Poolable> DisposePoolable;

    /// <summary>
    /// Subscribe the given method to the DisposeEvent
    /// </summary>
    /// <param name="f"> 
    /// Action to take when the poolable is disposed. It should
    /// receive a reference to the disposed object (aka this)
    /// </param>
    public void SubscribeDispose(Action<Poolable> f) => DisposePoolable += f;

    /// <summary>
    /// Desubscribe from the DisposeEvent
    /// </summary>
    /// <param name="f">Method to remove from the subscribers methods</param>
    public void DeSubscribeDispose(Action<Poolable> f) => DisposePoolable -= f;

    /// <summary>
    /// Delete this object from the scene. If it has a a pool, 
    /// </summary>
    public void Dispose()
    {
        if (!pooled)
        {
            DisposePoolable(this);
        }
        else
            throw new ArgumentException(
                    "Trying to dispose a poolable object that's already disposed"
                );
    }
    
}
