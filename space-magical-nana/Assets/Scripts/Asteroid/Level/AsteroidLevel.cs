using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLevel : Phase
{
    [SerializeField]
    private AsteroidPhaseSettingsSO _settings = null;
    [SerializeField]
    private ObjectPool _asteroidPool = null;

    private int _asteroidCount;


    private void OnEnable()
    {
        StartCoroutine(AsteroidSpawning());
    }


    private IEnumerator AsteroidSpawning()
    {
        float t = 0;
        while (t < _settings.Duration)
        {
            SpawnAsteroid();

            yield return new WaitForSeconds(_settings.MaxNextSpawn);
            
            t += _settings.MaxNextSpawn;
            Debug.Log(t);
        }
        Debug.Log("Waiting for asteroids to stop");
        yield return new WaitUntil(() => (_asteroidCount == 0));
        Debug.Log("No more asteroids!");
        End();
    }


    private void SpawnAsteroid()
    {
        Asteroid asteroid = _asteroidPool.Get().GetComponent<Asteroid>();
        asteroid.Spawn(3, 1.5f, 3, Random.Range(0.1f, 0.9f));
        asteroid.OnAsteroidDisposal += ReduceCount;
        ++_asteroidCount;
    }


    private void ReduceCount(Asteroid disposed)
    {
        disposed.OnAsteroidDisposal -= ReduceCount;
        --_asteroidCount;
    }
}
