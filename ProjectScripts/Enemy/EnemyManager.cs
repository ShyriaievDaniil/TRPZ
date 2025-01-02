using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, ISpawner
{
    [SerializeField] private List<Transform> _spawnPoints;
    private int _spawnCapacity;
    private List<Enemy> _spawnedEnemies;
    private List<EnemyData> _spawnableEnemies;
    private Coroutine _spawnCoroutine;


    public int SpawnCapacity => _spawnCapacity;
    public List<Enemy> SpawnedEnemies => _spawnedEnemies;
    public List<EnemyData> SpawnableEnemies => _spawnableEnemies;
    private EnemyData _bossData;

    public void Awake()
    {
        _spawnCapacity = 10;
        _spawnedEnemies = new List<Enemy>();
        _spawnableEnemies = new List<EnemyData>() { new EnemyData(20, 5, 10), new WarriorEnemyData(30, 7, 10), new NecromancerEnemyData(30, 5, 0)};
        EventBus.Summon += TrySummon;
        EventBus.LevelUp += InitBossStage;
        StartCoroutine(InitSpawning());
    }

    private IEnumerator InitSpawning()
    {
        foreach (EnemyData enemy in _spawnableEnemies) {
            SpawnNew(enemy.type);
            yield return new WaitForSeconds(1f);
        }
        _spawnCoroutine = StartCoroutine(StartSpawning());
    }
    private void InitBossStage(object obj, int level)
    {
        if (level >= 5)
        {
            StopSpawning();
            Player player = (Player)obj;
            _bossData = new BossEnemyDecorator(50, 0, 15, _spawnableEnemies[1]);
            Enemy boss = SpawnNew(_bossData.type, "Boss");
            boss._transform.position = player.transform.position + player.transform.forward*10;
            boss.SetData(_bossData);
        }
    }
    private void StopSpawning()
    {
        StopCoroutine(_spawnCoroutine);
        foreach (Enemy enemy in _spawnedEnemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector2 randomSpawnOffsetV2 = Random.insideUnitCircle * 3;
        Vector3 randomSpawnOffsetV3 = new Vector3(randomSpawnOffsetV2.x, 0, randomSpawnOffsetV2.y);
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)].position + randomSpawnOffsetV3;
    }

    public void TrySpawn()
    {
        foreach(Enemy enemy in _spawnedEnemies)
        {
            if (!enemy.gameObject.activeSelf)
            {
                Vector3 spawnPosition = GetSpawnPosition();
                enemy._transform.position = spawnPosition;
                enemy.SetData(_spawnableEnemies.Find(x => x.type == enemy.type));
                return;
            }
        }
        if(_spawnedEnemies.Count < _spawnCapacity-4)
        {
            EnemyData enemy = _spawnableEnemies[Random.Range(0, _spawnableEnemies.Capacity-1)];
            SpawnNew(enemy.type);
        }
    }
    public IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            TrySpawn();
            yield return new WaitForSeconds(2f);
        }
    }
    private Enemy SpawnNew(string type, string healthBarType = "Enemy")
    {
        GameObject enemyPrefab = Resources.Load<GameObject>($"Enemy/{type}");
        Enemy enemyObject = Instantiate(enemyPrefab).GetComponent<Enemy>();
        _spawnedEnemies.Add(enemyObject);
        enemyObject.SetType(type);
        EventBus.OnCreated(enemyObject, healthBarType);
        return enemyObject;
    }

    public void TrySummon(object obj, object summoner)
    {
        EnemyData enemyData = (EnemyData)obj;
        Enemy summonerData = (Enemy)summoner;

        if (_spawnedEnemies.Count < _spawnCapacity)
        {
            SpawnNew(enemyData.type);
        }
        foreach (Enemy enemy in _spawnedEnemies)
        {
            if ((!enemy.gameObject.activeSelf) && (enemy.type == enemyData.type))
            {
                Vector3 spawnPosition = summonerData._transform.position + summonerData._transform.forward * 3;
                enemy._transform.position = spawnPosition;
                enemy.SetData(enemyData);
                return;
            }
        }
    }
}
