using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]private GameObject _enemyPrefab;
    [SerializeField] private float _timeToSpawn;
    [SerializeField]private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    
    IEnumerator SpawnRoutine()
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

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
