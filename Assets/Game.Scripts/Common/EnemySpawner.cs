using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [Header("Config")]
    [SerializeField] private int limit;
    [SerializeField] private float timing = 0.25f;
    [SerializeField] private Vector2 maxRange = new(4, 3);
    [SerializeField] private Vector2 minRange = new(-4, -3);
    [SerializeField] private GameObject animPrefab;
    [SerializeField] private List<GameObject> enemyPrefabs = new();

    [Header("runtime debug")] 
    [SerializeField] private bool isRelease;
    [SerializeField] private List<GameObject> enemies = new();
    private float _currentTiming;
    

    public void Release()
    {
        isRelease = true;
        _currentTiming = timing;
    }

    public void StartSpawning()
    {
        isRelease = false;
    }

    public int PoolCount => enemies.Count;
 
    private void Update()
    {
        if(isRelease) return;
        
        RemoveUnused();
        if(enemies.Count >= limit) return;

        _currentTiming -= Time.deltaTime;
        if (_currentTiming <= (float)default)
        {
            var randX = Random.Range(minRange.x, maxRange.x);
            var randY = Random.Range(minRange.y, maxRange.y);
            var index = Random.Range(default, enemyPrefabs.Count);
            var position = new Vector3(randX, randY);
            
            Instantiate(animPrefab, position, Quaternion.identity);
            enemies.Add(Instantiate(enemyPrefabs[index], position, Quaternion.identity));
            _currentTiming = timing;
        }
    }

    private void RemoveUnused()
    {
        for (int i = default; i < enemies.Count; i++)
            if (enemies[i] is null || enemies[i].IsDestroyed())
            {
                enemies.Remove(enemies[i]);
                i--;
            }
    }
}