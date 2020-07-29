using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Future player movement controller component, for now is
/// just a test script for the moving entity
/// </summary>
public class PlayerMovementController : MonoBehaviour
{

    public Camera cam;
    public float toleranceRadius = 10;
    private MovingEntity movingEntity;

    private void Start()
    {
        movingEntity = GetComponent<MovingEntity>();
    }

    private void Update()
    {
        Vector2 input = cam.ScreenToWorldPoint( Input.mousePosition ) - 
                        transform.position;

        if ( Input.GetKey(KeyCode.Mouse0) && input.sqrMagnitude > toleranceRadius * toleranceRadius)
            movingEntity.Move(input);

        Debug.Log(input);
    }
}
