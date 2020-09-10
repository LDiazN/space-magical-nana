using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asteroid_Phase_", menuName = "Phases Settings/Asteroid Phase", order = 0)]
public class AsteroidPhaseSettingsSO : ScriptableObject
{
    [Min(0.1f)]
    [SerializeField]
    private float _minFreq = 1f; 
    [Min(0.1f)]
    [SerializeField]
    private float _maxFreq = 1f;
    [Min(0.1f)]
    [SerializeField]
    private float _minSize = 1f;
    [Min(0.1f)]
    [SerializeField]
    private float _maxSize = 1f;
    [Min(1)]
    [SerializeField]
    private int _minLife = 1;
    [Min(1)]
    [SerializeField]
    private int _maxLife = 1;
    [Min(0.1f)]
    [SerializeField]
    private float _minSpeed = 1f;
    [Min(0.1f)]
    [SerializeField]
    private float _maxSpeed = 1f;
    [Min(0.1f)]
    [SerializeField]
    private float _duration = 1f;

    public float MinNextSpawn { get { return _minFreq; } }
    public float MaxNextSpawn { get { return _maxFreq; } }

    public float MinSize { get { return _minSize; } }
    public float MaxSize { get { return _maxSize; } }
    
    public int MinLife { get { return _minLife; } }
    public int MaxLife { get { return _maxLife; } }

    public float MinSpeed { get { return _minSpeed; } }
    public float MaxSpeed { get { return _maxSpeed; } }

    public float Duration { get { return _duration; } }
}
