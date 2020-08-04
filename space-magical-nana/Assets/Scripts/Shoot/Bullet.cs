using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple base class to build bullets.
/// This is for testing only in the meanwhile
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public Vector2 velocity;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void  FixedUpdate()
    {
        rb2d.MovePosition( (Vector2) transform.position + velocity * Time.fixedDeltaTime);
    }
}
