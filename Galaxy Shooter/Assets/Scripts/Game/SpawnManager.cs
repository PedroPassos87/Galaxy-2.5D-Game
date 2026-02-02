using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Spawn Settings")]
    [SerializeField]public GameObject _enemyPrefab;
    [SerializeField]public GameObject _enemyContainer;
    [SerializeField] private float _baseSpawnRate; 
    private int _enemiesPerSpawn = 2; 
    private EnemyController _enemy; 


    public void Construct(GameObject enemyPrefab, GameObject enemyContainer, float baseSpawnRate, int enemiesPerSpawn)
    {
        _enemyPrefab = enemyPrefab;
        _enemyContainer = enemyContainer;
        _baseSpawnRate = baseSpawnRate;
        _enemiesPerSpawn = enemiesPerSpawn;
    }

    
    [Header("PowerUps Spawn Settings")]
    [SerializeField]private GameObject [] _powerupPrefabs;
    private bool _stopSpawning = false;
    private float _currentSpawnRate;

    [Header("Player stats")] 
    private PlayerController _player; 
    
    void Start()
    {
        if (_player == null)
        {
            _player = GameObject.Find("Player").GetComponent<PlayerController>();
        }
        
        if (_enemy == null)
        {
            _enemy = GameObject.Find("Enemy").GetComponent<EnemyController>();
        }
        
        if (_enemyPrefab == null || _enemyContainer == null)
        {
            Debug.LogError("Prefab ou container do inimigo não está atribuído.");
            return;
        }
        
        _currentSpawnRate = _baseSpawnRate;
        //StartCoroutine(SpawnPowerupRoutine());
        SpawnEnemies();
    }


    public void SpawnEnemies()
    {
        StartCoroutine(SpawnEnemyRoutine());

    }
    IEnumerator SpawnEnemyRoutine()
    {
        //infinite loop
        while (_stopSpawning == false)
        {
            for (int i = 0; i < _enemiesPerSpawn; i++)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 7, 0);
                GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
            }
            
            if (_player.GetScore() >= 50)
            {
                _enemiesPerSpawn = 4;
                _enemy.SetEnemySpeed(6);
            }
            else if (_player.GetScore() >= 20)
            {
                _enemiesPerSpawn = 2;
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