using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for semi-natural moving entities, such as the player
/// or enemies that go to a position rather than following a 
/// well-defined path. This class requires a RigidBody2D setted to kinematic
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MovingEntity : BaseMovingEntity
{
    // -- Self components ----------------
    private Rigidbody2D rb2d;
    // -----------------------------------


    // -- Editor Variables ---------------

    /// <summary>
    /// How fast this entity can move
    /// </summary>
    public float maxSpeed;  

    // -----------------------------------

    // -- Private Variables --------------

    // Current entity velocity
    private Vector2 velocity = Vector2.zero;

    // -----------------------------------

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
    /// Smoothly moves to the given position
    /// </summary>
    /// <param name="position"> World position to move at </param>
    public void MovePosition(in Vector2 position) =>
        velocity = (position - (Vector2)transform.position) * maxSpeed;
    /// <summary>
    /// Set the current speed to 0
    /// </summary>
    public void Stop() => velocity = Vector2.zero;

    // -- Abstract methods ---------------------

    public override void SetMaxSpeed(float newSpeed) => maxSpeed = newSpeed;

    public override float GetMaxSpeed() => maxSpeed;

    public override Vector2 GetVelocity() => velocity;




}
