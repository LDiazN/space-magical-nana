using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for semi-natural moving entities, such as the player
/// or enemies that go to a position rather than following a 
/// well-defined path. This class requires a RigidBody2D setted to kinematic
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MovingEntity : MonoBehaviour
{
    /// <summary>
    /// How fast this entity can move
    /// </summary>
    public float maxSpeed = 100f;

    /// <summary>
    /// How fast this entity reaches the max speed
    /// </summary>
    public float acceleration = 10.0f;

    // Current entity velocity
    private Vector2 velocity = Vector2.zero;

    // Rigidbody 2D setted to kinematic
    private Rigidbody2D rb2d;

    private void Start()
    {
        // init rigidbody
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition( 
                (Vector2) transform.position  + 
                velocity * Time.fixedDeltaTime 
        );
    }

    /// <summary>
    /// Move in the specified direction. 
    /// </summary>
    /// <param name="direction">
    /// Direction to move at. The vector is normalized, so 
    /// it have no effect to pass a direction with a significant speed
    /// </param>
    public void Move(in Vector2 direction)
    {
        Vector2 dir = direction.normalized;
        velocity += direction * acceleration;
    }
    
}
