using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemy : BaseEnemy
{

    /// <summary>
    /// The centinel point is a point in the screen 
    /// where the arena enemy will do its stuff. It's necessary
    /// since in the arena, the enemies will mostly stay at a fixed 
    /// position where they will stay until they die.
    /// </summary>
    [SerializeField]
    private Vector2 _centinelPoint;

    /// <summary>
    /// Point in the world where this enemy will spawn.
    /// </summary>
    [SerializeField]
    private Vector2 _spawnPoint;

    public override void Spawn()
    {
        transform.position = _spawnPoint;
        StartCoroutine(GoToCentinelPoint());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            _movingEntity.MovePosition(_centinelPoint);

    }

    private IEnumerator GoToCentinelPoint ()
    {
        while ((transform.position - (Vector3) _centinelPoint).sqrMagnitude > 0.1)
        {
            _movingEntity.MovePosition(_centinelPoint); // null reference
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        }
    }

}
