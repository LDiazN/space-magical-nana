using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asteroid_Sprites", menuName = "Asteroid Sprites", order = 0)]
public class AsteroidSpritesSO : ScriptableObject
{
    [SerializeField]
    private Sprite fullSprite = null;
    [SerializeField]
    private Sprite damagedSprite = null;

    public Sprite FullAsteroid { get { return fullSprite; } }
    public Sprite DamagedAsteroid { get { return damagedSprite; } }
}
