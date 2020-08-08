using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class that detects the player touch input and classiffies it.
/// </summary>
public class TouchDetector : MonoBehaviour
{
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
    
    private float startTime;
    private Vector3 startPos;
    
    /// <summary>
    /// Indicates if the touch already broke the movement treshold
    /// </summary>
    private bool brokeMovement;
    [SerializeField]
    private bool _isActive = true;


    /// <summary>
    /// Checks if the touch is till in the move treshold.
    /// </summary>
    /// <param name="start">Touch start position</param>
    /// <param name="actual">Touch actual position</param>
    /// <returns>true if the touch is still in the movement treshold/false otherwise</returns>
    public bool MoveTreshold(in Vector3 start, in Vector3 actual)
    {
        return Vector3.SqrMagnitude(actual - startPos) < _moveTreshold * _moveTreshold;
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


    private void Update()
    {
        if (!_isActive)
            return;

        TouchInput final;
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                TouchStatus status;
                Debug.Log($"Touch time: {Time.time - startTime}");

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        brokeMovement = false;
                        startTime = Time.time;
                        startPos = touch.position;
                        status = TouchStatus.Undefined;
                        break;

                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        if (MoveTreshold(startPos, touch.position))
                        {
                            brokeMovement = true;
                            status = TouchStatus.Continuous;
                        }
                        else if (TimeTreshold(startTime, Time.time) || brokeMovement)
                            status = TouchStatus.Continuous;
                        else
                            status = TouchStatus.Undefined;
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if (TimeTreshold(startTime, Time.time) && MoveTreshold(startPos, touch.position) && !brokeMovement)
                        {
                            Debug.Log("Is tap!");
                            status = TouchStatus.Tap;
                        }
                        else
                            status = TouchStatus.Continuous;

                        brokeMovement = false;
                        break;

                    default:
                        throw new Exception("WTF");
                }
                final = new TouchInput(status, touch);
            }
        }
        else
            final = new TouchInput(TouchStatus.None, null);
    }
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