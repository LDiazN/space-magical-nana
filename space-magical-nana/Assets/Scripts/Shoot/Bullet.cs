using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple base class to build bullets.
/// This is for testing only in the meanwhile
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Poolable))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public Vector2 velocity;

    public float distanceFromPlayer = 5f;

    private GameObject player;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.Find("SamplePlayer");
    }

    private void Update()
    {
        if ((transform.position - player.transform.position).magnitude > distanceFromPlayer)
            GetComponent<Poolable>().Dispose();
    }

    // Update is called once per frame
    void  FixedUpdate()
    {
        rb2d.MovePosition( (Vector2) transform.position + velocity * Time.fixedDeltaTime);
    }
}
