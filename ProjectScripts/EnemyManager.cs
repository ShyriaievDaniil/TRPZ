using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;

    public void Awake()
    {
        _spawnPoints[0].SetEnemy(new EnemyData(20, 5, 10));
        _spawnPoints[1].SetEnemy(new WarriorEnemyData(30, 7, 10));
        _spawnPoints[2].SetEnemy(new NecromancerEnemyData(30, 5, 0));
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            foreach (SpawnPoint spawnPoint in _spawnPoints) {
                if (spawnPoint.CanSpawn())
                {
                    spawnPoint.Spawn();
                }
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
