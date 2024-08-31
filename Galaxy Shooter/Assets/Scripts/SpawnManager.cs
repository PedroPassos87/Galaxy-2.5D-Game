using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Spawn Settings")]
    [SerializeField]private GameObject _enemyPrefab;
    [SerializeField]private GameObject _enemyContainer;
    [SerializeField] private float _timeToSpawn;
    
    [Header("PowerUps Spawn Settings")]
    [SerializeField]private GameObject _tripleShotPowerupPrefab;

    private bool _stopSpawning = false;
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    
    IEnumerator SpawnEnemyRoutine()
    {
        //infinite loop
        while (_stopSpawning == false)
        {
            //definindo posiçao em que o inimigo spawna (entre -8 e 8 em x, 7 em y)
            Vector3 spawnPos = new Vector3(Random.Range(-8f,8f),7,0);
            //instanciando inimigo prefab
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            //yield wait 5 seconds
            yield return new WaitForSeconds(_timeToSpawn);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8f,8f),7,0);
            Instantiate(_tripleShotPowerupPrefab, postToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true; 
    }
}
