using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    private float _speed;
    private Vector2 _direction;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();    
    }


    public void GoToAndMove(Vector2 dir, Vector2 pos, float speed)
    {
        transform.position = pos;
        //rb2d.MovePosition(pos);
        _direction = dir.normalized;
        _speed = speed;
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition((Vector2)transform.position + _direction * _speed * Time.fixedDeltaTime);
    }
}
