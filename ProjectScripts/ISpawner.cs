using System.Collections.Generic;
using UnityEngine;

public interface ISpawner
{
    public int SpawnCapacity { get; }
    public List<GameObject> SpawnedEntities { get; }
    public List<EnemyData> SpawnableEntities { get; }

    public bool CanSpawn();
    public void Spawn();
}
