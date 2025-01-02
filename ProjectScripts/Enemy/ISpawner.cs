using System.Collections.Generic;

public interface ISpawner
{
    public int SpawnCapacity { get; }
    public List<Enemy> SpawnedEnemies { get; }
    public List<EnemyData> SpawnableEnemies { get; }

    public void TrySpawn();
}
