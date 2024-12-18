using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour, ISpawner
{
    private int _spawnCapacity;
    private List<GameObject> _spawnedEntities;
    private List<EnemyData> _spawnableEntities;
    private Transform _transform;

    public int SpawnCapacity => _spawnCapacity;

    public List<GameObject> SpawnedEntities => _spawnedEntities;

    public List<EnemyData> SpawnableEntities => _spawnableEntities;
    public void Awake()
    {
        _spawnCapacity = 1;
        _transform = transform;
        _spawnedEntities = new List<GameObject>();
    }

    public void SetEnemy(EnemyData data)
    {
        _spawnableEntities = new List<EnemyData>();
        _spawnableEntities.Add(data);
    }

    public bool CanSpawn()
    {
        if(_spawnableEntities.Count != 0) { 
            if (_spawnedEntities.Count < _spawnCapacity)
            {
                return true;
            }
            foreach (GameObject entity in _spawnedEntities)
            {
                if (!entity.activeSelf)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Spawn()
    {
        if (_spawnedEntities.Count < _spawnCapacity)
        {
            GameObject minion = Resources.Load<GameObject>($"Enemy/{_spawnableEntities[0].type}");
            GameObject SpawnedMinion = Object.Instantiate(minion, _transform.position, Quaternion.identity);
            _spawnedEntities.Add(SpawnedMinion);
        }
            foreach (GameObject entity in _spawnedEntities)
        {
            if (!entity.activeSelf)
            {
                entity.transform.position = _transform.position;
                entity.GetComponent<Enemy>().SetData((EnemyData)_spawnableEntities[0].Clone());
                break;
            }
        }
    }
}
