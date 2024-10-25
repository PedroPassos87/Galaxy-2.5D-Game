using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Spawn Settings")]
    [SerializeField]private GameObject _enemyPrefab;
    [SerializeField]private GameObject _enemyContainer;
    
    [SerializeField] private float _baseSpawnRate; // Spawn rate inicial em segundos
    [SerializeField] private int _enemiesPerSpawn; // Quantos inimigos spawnam a cada ciclo

    public void Construct(GameObject enemyPrefab, float baseSpawnRate, int enemiesPerSpawn)
    {
        _enemyPrefab = enemyPrefab;
        _baseSpawnRate = baseSpawnRate;
        _enemiesPerSpawn = enemiesPerSpawn;
    }

    [Header("PowerUps Spawn Settings")]
    [SerializeField]private GameObject [] _powerupPrefabs;
    private bool _stopSpawning = false;
    
    private float _currentSpawnRate;
    
    void Start()
    {
        _currentSpawnRate = _baseSpawnRate;
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    
    IEnumerator SpawnEnemyRoutine()
    {
        //aplicar verificacao de score
        
        //infinite loop
        while (_stopSpawning == false)
        {
            for (int i = 0; i < _enemiesPerSpawn; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
            }
            //yield return new WaitForSeconds(_timeToSpawn);
            yield return new WaitForSeconds(_currentSpawnRate);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8f,8f),7,0);
            int RandomPowerUp = Random.Range(0, _powerupPrefabs.Length);
            Instantiate(_powerupPrefabs[RandomPowerUp], postToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true; 
    }
}