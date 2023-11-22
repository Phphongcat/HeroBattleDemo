using System.Collections.Generic;
using QtNameSpace;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [Header("Config")]
    [SerializeField] private int sizeLimit;
    [SerializeField] private float timing = 4f;
    [SerializeField] private float decrementTiming = 0.25f;
    [SerializeField] private float minLimitTiming = 2f;
    [SerializeField] private int decrementPerEnemyDeadTiming = 5;
    [SerializeField] private Vector2 maxRange = new(4, 3);
    [SerializeField] private Vector2 minRange = new(-4, -3);
    [SerializeField] private SpawnTower animPrefab;
    [SerializeField] private List<GameObject> enemyPrefabs = new();

    [Header("Runtime Debug")] 
    [SerializeField] private float limitTiming;
    [SerializeField] private int enemyDeadCount;
    [SerializeField] private bool isRelease;
    [SerializeField] private List<GameObject> enemies = new();
    private float _currentTiming;
    

    public void Release()
    {
        isRelease = true;
    }

    public void StartSpawning()
    {
        _currentTiming = limitTiming = timing;
        enemyDeadCount = default;
        isRelease = false;
    }

    public int PoolCount => enemies.Count;
 
    private void Start()
    {
        Release();
    }

    private void Update()
    {
        if(isRelease) return;
        
        RemoveUnused();
        if(enemies.Count >= sizeLimit) return;

        _currentTiming -= Time.deltaTime;
        if (_currentTiming <= (float)default)
        {
            var randX = Random.Range(minRange.x, maxRange.x);
            var randY = Random.Range(minRange.y, maxRange.y);
            var position = new Vector3(randX, randY);
            var tower = Instantiate(animPrefab, position, Quaternion.identity).GetComponent<SpawnTower>();
            tower.Action(SpawnEnemy);
            
            DecrementTimingIfCan();
            _currentTiming = limitTiming;
        }
    }

    private void SpawnEnemy(Vector3 position)
    {
        var index = Random.Range(default, enemyPrefabs.Count);
        enemies.Add(Instantiate(enemyPrefabs[index], position, Quaternion.identity));
    }

    private void RemoveUnused()
    {
        for (int i = default; i < enemies.Count; i++)
            if (enemies[i] is null || enemies[i].IsDestroyed())
            {
                enemies.Remove(enemies[i]);
                enemyDeadCount++;
                i--;
            }
    }

    private void DecrementTimingIfCan()
    {
        var delta = enemyDeadCount / decrementPerEnemyDeadTiming;
        var decrementValue = delta * decrementTiming;
        var newLimitTiming = timing - decrementValue;
        limitTiming = Mathf.Clamp(newLimitTiming, minLimitTiming, timing);
    }
}