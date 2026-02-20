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
        if (enemyPrefab == null || path == null || spawnPoint == null)
        {
            Debug.LogError($"EnemySpawner missing refs. enemyPrefab={enemyPrefab} path={path} spawnPoint={spawnPoint}");
            enabled = false;
            return;
        }

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        var follow = enemy.GetComponent<EnemyFollowPath>();
        if (follow == null)
        {
            Debug.LogError("Spawned enemyPrefab is missing EnemyFollowPath.");
            Destroy(enemy);
            enabled = false;
            return;
        }

        follow.Init(path);
        spawned++;
    }
}
