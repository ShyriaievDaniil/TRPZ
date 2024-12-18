using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerEnemyData : EnemyData, ISpawner
{
    private int _spawnCapacity;
    private List<GameObject> _spawnedEntities;
    private List<EnemyData> _spawnableEntities;
    public int SpawnCapacity => _spawnCapacity;

    public List<GameObject> SpawnedEntities => _spawnedEntities;

    public List<EnemyData> SpawnableEntities => _spawnableEntities;
    public NecromancerEnemyData(int maxHealth, int speed, int damage) : base(maxHealth, speed, damage)
    {
        _spawnCapacity = 3;
        _spawnedEntities = new List<GameObject>();
        _spawnableEntities = new List<EnemyData>();
        _spawnableEntities.Add(new EnemyData(10, 5, 5));
        type = "Skeleton_Mage";
    }

    public override IEnumerator Attack(Transform targetPosition, Enemy owner)
    {
        while (true)
        {
            if ((targetPosition.position - owner.transform.position).magnitude <= 5 && CanSpawn())
            {
                isAttacking = true;
                EventBus.OnAttacking(owner);
                yield return new WaitForSeconds(1f);
                Spawn(owner);
                isAttacking = false;
                yield return new WaitForSeconds(2f);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public bool CanSpawn()
    {
        if(_spawnedEntities.Count < _spawnCapacity)
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
        return false;
    }

    public void Spawn(Enemy owner)
    {
        if(_spawnedEntities.Count < _spawnCapacity)
        {
            GameObject minion = Resources.Load<GameObject>($"Enemy/{_spawnableEntities[0].type}");
            GameObject SpawnedMinion = Object.Instantiate(minion);
            _spawnedEntities.Add(SpawnedMinion);
        }
        foreach (GameObject entity in _spawnedEntities) {
            if (!entity.activeSelf) {
                entity.transform.position = owner.transform.position + owner.transform.forward * 3;
                entity.GetComponent<Enemy>().SetData((EnemyData)_spawnableEntities[0].Clone());
                break;
            }
        }
    }
    public override ISpawnable Clone()
    {
        return new NecromancerEnemyData(maxHealth, speed, damage);
    }
    public void Spawn()
    {
        throw new System.NotImplementedException();
    }
}
