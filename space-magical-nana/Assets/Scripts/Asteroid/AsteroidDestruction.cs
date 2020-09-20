using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestruction : MonoBehaviour
{
    private static AsteroidSpritesSO[] s_sprites = null;

    private SpriteRenderer _renderer = null;
    private BaseHealthSystem _hpSys = null;

    private AsteroidSpritesSO _actualSprites = null;
    private bool _changed = false;

    private void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        if (!_renderer)
            throw new NullReferenceException("Asteroid renderer missing");

        if (TryGetComponent(out  _hpSys))
        {
            _hpSys.OnLifeChange += UpdateSprite;
            _hpSys.OnNoHP += DestroyAsteroid;
        }
        else
            throw new NullReferenceException("Asteroid HP missing");

        if (s_sprites == null)
            s_sprites = Resources.LoadAll<AsteroidSpritesSO>("Asteroids");

        GetNewSprite();
    }


    private void UpdateSprite(int hp)
    {
        if (0 < hp && hp <= (int)_hpSys.MaxHP/2 && !_changed)
        {
            _renderer.sprite = _actualSprites.DamagedAsteroid;
            _changed = true;
        }
    }

    private void DestroyAsteroid(int hp)
    {
        if (hp != 0)
            return;
        // kaboom.
    }


    public void ResetDestruction()
    {
        _changed = false;
        _renderer.sprite = _actualSprites.FullAsteroid;
    }


    public void GetNewSprite() => _actualSprites = s_sprites[UnityEngine.Random.Range(0, s_sprites.Length - 1)];
}

