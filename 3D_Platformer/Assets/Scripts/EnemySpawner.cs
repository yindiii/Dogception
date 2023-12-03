using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 5.0f;
    public Transform[] spawnPoints;
    public Transform playerTransform; 

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, spawnInterval);
    }

    void SpawnEnemy()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().target = playerTransform; // Set the player as the target
    }
}
