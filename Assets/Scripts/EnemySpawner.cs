using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Path path;
    [SerializeField] private Transform spawnPoint;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 1.0f;
    [SerializeField] private int amountToSpawn = 20;

    private float timer;
    private int spawned;

    private void Update()
    {
        if (spawned >= amountToSpawn) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnOne();
        }
    }

    private void SpawnOne()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        enemy.GetComponent<EnemyFollowPath>().Init(path);
        spawned++;
    }
}
