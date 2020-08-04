using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class to provide an interface to get and return 
/// pooled objects. 
/// </summary>
public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// Stored objects
    /// </summary>
    private Stack<GameObject> objects;

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
        // Create & fill the object stack
        objects = new Stack<GameObject>(initialObjectCount);
        for (int i = 0; i < initialObjectCount; i++)
        {
            var o = Instantiate(pooledObject);
            objects.Push(o);
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
            var o = objects.Pop();
            o.SetActive(true);
            return o;
        }

        return Instantiate(pooledObject);
    }
    /// <summary>
    /// Return a game object to the pool
    /// </summary>
    /// <param name="gameObject"> Object to return </param>
    public void Dispose(GameObject gameObject)
    {
        gameObject.SetActive(false);
        objects.Push(gameObject);
    }

}
