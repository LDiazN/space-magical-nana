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
        // We're only interested in a single touch, so we use 
        // only the first touch 
        if (Input.touchCount > 0)
        {
            Debug.Log("Putisima");
            Touch touchInfo = Input.GetTouch(0);
            ManageTouch(touchInfo);
        }
        else
            movingEntity.Stop();
    }

    private void ManageTouch(in Touch touchInfo)
    {
        Debug.Log("Gordo");
        switch (touchInfo.phase)
        {
            case TouchPhase.Began:
            case TouchPhase.Moved:
            case TouchPhase.Stationary:
                MovePlayer(touchInfo);
                break;
            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                movingEntity.Stop();
                break;
            default:
                break;
        }
    }


    private void MovePlayer(in Touch touchInfo)
    {
        movingEntity.MovePosition((Vector2)transform.position + CalculateDir(touchInfo.position));
    }

    private Vector2 CalculateDir(in Vector2 touchPos)
    {
        return (Camera.main.ScreenToWorldPoint(touchPos) - transform.position);
    }
}
