using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;


namespace Tests
{
    public class EnemySpawnerTest
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator _Instantiates_GameObject_From_Prefab()
        {
            // Use the Assert class to test conditions
            var enemyPrefab = Resources.Load<GameObject>("Tests/enemy");
            var enemySpawner = new GameObject().AddComponent<SpawnManager>();
            enemySpawner.Construct(enemyPrefab, 5, 1);
        
            yield return null;
            
            var spawnedEnemy = GameObject.FindWithTag("Enemy");
            var prefabOfTheSpawnedEnemy = PrefabUtility.GetCorrespondingObjectFromSource(spawnedEnemy);
            Assert.AreEqual(enemyPrefab,prefabOfTheSpawnedEnemy);
            
        }
    }
    
}