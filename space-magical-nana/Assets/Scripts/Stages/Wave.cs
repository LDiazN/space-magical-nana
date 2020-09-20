using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : Phase
{
    /// <summary>
    /// Called whenever a wave starts
    /// </summary>
    protected abstract void DeployWave();

    /// <summary>
    /// Called when this wave is ended
    /// </summary>
    protected abstract void WaveEnded();

}
