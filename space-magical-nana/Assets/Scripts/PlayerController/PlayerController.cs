using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This class controls the flow of the main player ship,
/// so this is a wrapper for all its individual components and subsystems
/// </summary>
[RequireComponent(typeof(MovingEntity), typeof(TouchDetector), typeof(WeaponManager))]
[RequireComponent(typeof(Ship))] // The ship component that manage Ship stats information 
public class PlayerController : MonoBehaviour
{ 
    // -- Self Components --------------------

    /// <summary>
    /// Required to move the player aroind the screen
    /// </summary>
    private MovingEntity _movingEntity;

    /// <summary>
    /// Required to get the input state
    /// </summary>
    private TouchDetector _detector;

    /// <summary>
    /// Object to manage shooting 
    /// </summary>
    private WeaponManager _weaponManager;

    // -- Editor Fields ----------------------

    /// <summary>
    /// Scene camera. Required to compute word position 
    /// based on camera coordinates
    /// </summary>
    [SerializeField]
    private Camera _cam;

    // -- Public fields ----------------------

    /// <summary>
    /// Event called whenever the player enters in Slow Motion state 
    /// </summary>
    public event Action EnteringSlowMo;

    /// <summary>
    /// Event called whenever the player enters in regular gameplay state
    /// </summary>
    public event Action EnteringFiring;

    // -- Private fields ---------------------

    /// <summary>
    /// All the possible player states
    /// </summary>
    private enum PlayerState
    {
        Firing, // Continuos input and regular playing, firing the main weapon
        SlowMo  // None input, choosing a possible skill to use and slow motion.
    }

    private PlayerState state = PlayerState.SlowMo;

    // -- In game Execution ------------------
    private void Start()
    {
        // Initializing self components:
        _movingEntity = GetComponent<MovingEntity>();
        _detector = GetComponent<TouchDetector>();
        _weaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        // Update the current state:
        UpdateState();

        // Perform every subsystem actions
        Movement();
        Attack();
        

    }

    

    // -- Subsystems -------------------------

    /// <summary>
    /// Update the current player state and other stateful information 
    /// </summary>
    protected virtual void UpdateState()
    {
        var inputState = _detector.ProcessInput();

        switch(inputState.type)
        {
            case TouchStatus.Continuous:
                if ( state != PlayerState.Firing)
                {
                    state = PlayerState.Firing;
                    OnEnteringFiring();
                }
                break;
            default:
                if (state != PlayerState.SlowMo)
                {
                    state = PlayerState.SlowMo;
                    OnEnteringSlowMo();
                }
                break;
        }  
    }

    /// <summary>
    /// Manage the movement logic and use the movingEntity component
    /// </summary>
    protected virtual void Movement()
    {
        if (state != PlayerState.Firing)
        {
            _movingEntity.Stop();
            return;
        }


        var touchInfo = _detector.ProcessInput().touch;

        if (touchInfo == null)
            return;

        var screenPosition = touchInfo.Value.position;
        var worldPosition = _cam.ScreenToWorldPoint(screenPosition);

        Debug.Log("Input position is: " + worldPosition);

        _movingEntity.MovePosition(worldPosition);
    }

    protected virtual void Attack()
    {
        if (state != PlayerState.Firing)
            return;

        _weaponManager.Shoot();
    }

    // -- Aux Functions ---------------------------------

    /// <summary>
    /// Call the EnteringSlowMo event
    /// </summary>
    private void OnEnteringSlowMo()
    {
        EnteringSlowMo();
    }

    /// <summary>
    /// Call the EnteringFiring event
    /// </summary>
    private void OnEnteringFiring()
    {
        EnteringFiring();
    }
}
