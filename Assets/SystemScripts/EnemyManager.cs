using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Queue<GameObject> enemies = new Queue<GameObject>();
    GameObject enemyPrefab;

    int enemyCount = 0;
    int enemyLimit = 60;

    private void Awake()
    {
        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
    }

    void ResetEnemyForSpawn(GameObject enemy)
    {
        enemy.GetComponent<BoxCollider2D>().enabled = true;
        enemy.GetComponent<CharacterStats>().ResetHealth();
        enemy.SetActive(true);
    }

    bool CreateEnemy()
    {
        if (enemyCount >= enemyLimit)
        {
            return false;
        }

        GameObject enemy = Instantiate(enemyPrefab);
        AddEnemyToQueue(enemy);
        enemyCount++;

        return true;
    }

    public void AddEnemyToQueue(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.GetComponent<BoxCollider2D>().enabled = false;
        enemies.Enqueue(enemy);
    }

    public GameObject GetEnemyFromQueue()
    {
        if (enemies.Count == 0)
        {
            if (!CreateEnemy())
            {
                return null;
            }
        }

        GameObject enemy = enemies.Dequeue();
        ResetEnemyForSpawn(enemy);

        return enemy;
    }
}