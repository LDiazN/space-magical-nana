using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Future player movement controller component, for now is
/// just a test script for the moving entity
/// </summary>
[RequireComponent(typeof(MovingEntity))]
public class PlayerMovementController : MonoBehaviour
{
    // -- Self Components ------------------
    private MovingEntity movingEntity;
    // -------------------------------------

    // -- Editor Variables -----------------

    /// <summary>
    /// The camera that the player is using
    /// </summary>
    public Camera cam;

    // -------------------------------------

    private void Start()
    {
        movingEntity = GetComponent<MovingEntity>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            movingEntity.MovePosition(cam.ScreenToWorldPoint(Input.mousePosition));
        else
            movingEntity.Stop();
    }
}
