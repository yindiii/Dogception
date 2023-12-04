using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1.0f;
    public Transform[] spawnPoints;
    public Transform playerTransform;
    public bool canSpawn = true;

    void Start()
    {
        StartCoroutine(SpawnEnemies()); // Start the coroutine here
    }

    IEnumerator SpawnEnemies()
    {
        while (canSpawn)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // Wait for the interval
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return; // Safety check

        int spawnIndex = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().target = playerTransform; // Set the player as the target
    }

    public void StopSpawning()
    {
        canSpawn = false; // This will stop the while loop in the coroutine
    }

    public void StartSpawning()
    {
        if (!canSpawn)
        {
            canSpawn = true;
            StartCoroutine(SpawnEnemies()); // Restart the spawning coroutine if it was stopped
        }
    }
}
