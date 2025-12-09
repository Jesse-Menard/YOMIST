using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    float spawnDelay = 2.0f;
    [SerializeField]
    int spawnQuantity = 1;
    [SerializeField]
    float spawnDelayDecrement = 0.95f;
    List<Vector3> spawnerPositions = new List<Vector3>();

    private void Start()
    {
        EnemySpawnPoint[] foundSpawners = FindObjectsByType<EnemySpawnPoint>(FindObjectsSortMode.None);

        foreach (EnemySpawnPoint foundSpawner in foundSpawners) 
        {
            spawnerPositions.Add(foundSpawner.transform.position + foundSpawner.spawnOffset);
        }

        StartCoroutine(SpawnerRoutine());
    }

    IEnumerator SpawnerRoutine()
    {
        for (int i = 0; i < spawnQuantity; i++)
        {
            SpawnEnemy();
        }

        yield return new WaitForSeconds(spawnDelay);


        if (spawnDelay > 1.0f / 4.0f)
        {
            spawnDelay *= spawnDelayDecrement;
        }

        StartCoroutine(SpawnerRoutine());
    }

    void SpawnEnemy()
    {
        int randInt = Random.Range(0, spawnerPositions.Count);
        GameObject enemy = GetComponent<EnemyManager>().GetEnemyFromQueue();

        if (enemy)
            enemy.transform.position = spawnerPositions.ElementAt(randInt);
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnerRoutine());
    }
}