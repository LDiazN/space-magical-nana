using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingEntityv2 : MonoBehaviour
{
    // Local Components

    private Rigidbody2D rb2d;

    // ---------------------------------------------

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
