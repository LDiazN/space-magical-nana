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
    /// This event is called when the object should be deleted
    /// </summary>
    event Action<GameObject> DisposePoolable;

    /// <summary>
    /// Subscribe the given method to the DisposeEvent
    /// </summary>
    /// <param name="f"> 
    /// Action to take when the poolable is disposed. It should
    /// receive a reference to the disposed object (aka this)
    /// </param>
    public void SubscribeDispose(Action<GameObject> f) => DisposePoolable += f;

    /// <summary>
    /// Desubscribe from the DisposeEvent
    /// </summary>
    /// <param name="f">Method to remove from the subscribers methods</param>
    public void DeSubscribeDispose(Action<GameObject> f) => DisposePoolable -= f;

    /// <summary>
    /// Delete this object from the scene. If it has a a pool, 
    /// use the pools dispose. Otherwise destroy the object itself.
    /// </summary>
    public void Dispose() => DisposePoolable(gameObject);
    
}
