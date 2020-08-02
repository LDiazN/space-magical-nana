using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a test code for now, we have to change this
/// </summary>
public class Stage : Phase
{
    public int id;

    float timePassed = 0;
    private void Update()
    {
        if (timePassed >= 5f)
        {
            End();
            Debug.Log("I'm stage " + id + " and i've just ended");

        }

        timePassed += Time.deltaTime;
    }
}
