using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class that detects the player touch input and classiffies it.
/// </summary>
public class TouchDetector : MonoBehaviour
{
    private bool _dirtyTouch = false;
    private TouchInput _calculated;

    /// <summary>
    /// How much time can pass before a touch is
    /// not considered a tap.
    /// </summary>
    [SerializeField]
    [Min(0f)]
    private float timeTreshold = 0.09f;
    /// <summary>
    /// How far can a touch be from the start to
    /// still be considered a tap.
    /// </summary>
    [SerializeField]
    [Min(0f)]
    private float _moveTreshold = 0.1f;
    
    private float _startTime;
    private Vector3 _startPos;
    
    /// <summary>
    /// Indicates if the touch already broke the movement treshold
    /// </summary>
    private bool _brokeMovement;

    public delegate void receiveInput(TouchInput input);
    public event receiveInput InputReceived;


    private void Start()
    {
        #if UNITY_EDITOR
        InputReceived += PrintStatus;
        #endif
    }


    private void Update()
    {
        InputReceived?.Invoke(ProcessInput());
    }


    private void LateUpdate()
    {
        _dirtyTouch = false;
    }


    public TouchInput ProcessInput()
    {
        if (_dirtyTouch)
            return _calculated;

        Touch touch;
        TouchStatus status = TouchStatus.None;

        if (Input.touches.Length > 0)
        {
            touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _brokeMovement = false;
                    _startTime = Time.time;
                    _startPos = touch.position;
                    status = TouchStatus.Undefined;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (!MoveTreshold(_startPos, touch.position))
                    {
                        _brokeMovement = true;
                        status = TouchStatus.Continuous;
                    }
                    else if (!TimeTreshold(_startTime, Time.time) || _brokeMovement)
                        status = TouchStatus.Continuous;
                    else
                        status = TouchStatus.Undefined;
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (TimeTreshold(_startTime, Time.time) && MoveTreshold(_startPos, touch.position))
                        status = TouchStatus.Tap;
                    else
                        status = TouchStatus.Continuous;

                    _brokeMovement = false;
                    break;

                default:
                    throw new Exception("Impossible enum value");
            }
            _calculated = new TouchInput(status, touch);
        }
        else
            _calculated = new TouchInput(status, null);

        _dirtyTouch = true;
        return _calculated;
    }


    /// <summary>
    /// Checks if the touch is still in the move treshold.
    /// </summary>
    /// <param name="start">Touch start position</param>
    /// <param name="actual">Touch actual position</param>
    /// <returns>true if the touch is still in the movement treshold/false otherwise</returns>
    public bool MoveTreshold(in Vector3 start, in Vector3 actual)
    {
        return Vector3.SqrMagnitude(actual - _startPos) < _moveTreshold * _moveTreshold;
    }


    /// <summary>
    /// Checks if the touch is still below the tap treshold.
    /// </summary>
    /// <param name="start">Time that the touch started</param>
    /// <param name="actual">Time of the actual touch</param>
    /// <returns>true if the touch time is below the treshold/false otherwise</returns>
    public bool TimeTreshold(float start, float actual)
    {
        return (actual - start) < timeTreshold;
    }


    private void ChangeActiveStatus(bool status)
    {
        enabled = status;
    }


#if UNITY_EDITOR
    private void PrintStatus(TouchInput input)
    {
        Debug.Log(Enum.GetName(typeof(TouchStatus), input.type));
    }
#endif
}


/// <summary>
/// Different status of a touch.
/// </summary>
public enum TouchStatus
{
    None,
    Undefined,
    Tap,
    Continuous,
}


/// <summary>
/// Struct containing info about a touch and it status.
/// </summary>
public struct TouchInput
{
    public TouchStatus type;
    public Touch? touch;

    public TouchInput(TouchStatus type, Touch? touch)
    {
        this.type = type;
        this.touch = touch;
    }
}