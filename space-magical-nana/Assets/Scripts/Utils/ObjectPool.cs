using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

/// <summary>
/// Simple class to provide an interface to get and return 
/// pooled objects. 
/// </summary>
public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// Stored objects
    /// </summary>
    private Stack<Poolable> objects;

    /// <summary>
    /// how many objcts will be initially available. The pool will resize if more 
    /// objects are requested 
    /// </summary>
    public int initialObjectCount = 0;

    /// <summary>
    /// Object to replicate
    /// </summary>
    [SerializeField]
    private GameObject pooledObject;

    void Awake()
    {
        // Check if the given gameobject is a valid one
        var poolableComp = pooledObject?.GetComponent<Poolable>();

        if (pooledObject == null)
            throw new ArgumentNullException(
                "The given pooled object is null, it should be set to an object " +
                "with a Poolable component"
                );
        else if (poolableComp == null)
            throw new ArgumentException(
                "The provided pooled object does not have a poolable component"
            );

        // Create & fill the object stack
        objects = new Stack<Poolable>(initialObjectCount);
        
        for (int i = 0; i < initialObjectCount; i++)
        {
            // Create the object
            var o = Instantiate(pooledObject);

            // get its poolable and subscribe to its event
            poolableComp = o.GetComponent<Poolable>();
            poolableComp.SubscribeDispose( Dispose );
            poolableComp.pooled = true;

            // Push the object into the stack and deactivate it
            objects.Push(poolableComp);
            o.SetActive(false);
        }
    }

    /// <summary>
    /// Get a game object from the pool. If the pool is empty, create a new one
    /// </summary>
    /// <returns> A game object from the pool </returns>
    public GameObject Get()
    {
        if (objects.Count > 0)
        {
            var poolable = objects.Pop();
            poolable.gameObject.SetActive(true);
            poolable.pooled = false;
            return poolable.gameObject;
        }

        // If there's not enough objects in the pool, create a new one.
        var newObj = Instantiate(pooledObject);
        var poolableComp = newObj.GetComponent<Poolable>();
        poolableComp.SubscribeDispose(Dispose);
        poolableComp.pooled = false;
        return newObj;
    }

    /// <summary>
    /// Return a game object to the pool
    /// </summary>
    /// <param name="gameObject"> Object to return </param>
    private void Dispose(Poolable poolable)
    {
        poolable.gameObject.SetActive(false);
        objects.Push(poolable);
        poolable.pooled = true;
    }

}
