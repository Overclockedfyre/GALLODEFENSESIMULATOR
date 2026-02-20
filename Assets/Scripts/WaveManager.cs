using System.Collections;
using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Path path;
    [SerializeField] private TMP_Text waveText;

    [SerializeField] private bool autoStart = true;
    [SerializeField] private bool autoNextWave = true;

    [SerializeField] private string enemyTag = "Enemy";

    public int CurrentWaveIndex { get; private set; } = -1;
    public bool IsSpawning { get; private set; }
    public int CurrentWaveNumber => CurrentWaveIndex + 1; // 1-based for UI
    public int TotalWaves => waveConfig != null ? waveConfig.waves.Count : 0;

    private Coroutine waveRoutine;

    void Start()
    {
        UpdateWaveUI();
        if (autoStart) StartNextWave();
    }

    public void StartNextWave()
    {
        if (waveConfig == null || waveConfig.waves.Count == 0) return;
        if (IsSpawning) return;

        int next = CurrentWaveIndex + 1;
        if (next >= waveConfig.waves.Count) return;

        CurrentWaveIndex = next;
        UpdateWaveUI();

        if (waveRoutine != null) StopCoroutine(waveRoutine);
        waveRoutine = StartCoroutine(RunWave(waveConfig.waves[CurrentWaveIndex]));
    }

    private IEnumerator RunWave(Wave wave)
    {
        IsSpawning = true;

        if (wave.preDelay > 0f)
            yield return new WaitForSeconds(wave.preDelay);

        for (int g = 0; g < wave.groups.Count; g++)
        {
            var group = wave.groups[g];
            if (group.enemyPrefab == null || group.count <= 0) continue;

            for (int i = 0; i < group.count; i++)
            {
                Spawn(group.enemyPrefab);
                if (group.spawnInterval > 0f)
                    yield return new WaitForSeconds(group.spawnInterval);
            }

            if (wave.interGroupDelay > 0f)
                yield return new WaitForSeconds(wave.interGroupDelay);
        }

        IsSpawning = false;

        yield return new WaitUntil(() => CountAliveEnemies() == 0);

        if (autoNextWave)
            StartNextWave();
    }

    private void Spawn(GameObject enemyPrefab)
    {
        if (spawnPoint == null) return;

        GameObject e = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Assign Path to the enemy's follow script
        var follow = e.GetComponent<EnemyFollowPath>();
        if (follow != null)
        {
            follow.Init(path);
        }
        else
        {
            Debug.LogWarning($"Spawned enemy {e.name} has no EnemyFollowPath component.");
        }
    }
    private int CountAliveEnemies()
    {
        return GameObject.FindGameObjectsWithTag(enemyTag).Length;
    }
    private void UpdateWaveUI()
    {
        if (waveText == null) return;

        int current = Mathf.Clamp(CurrentWaveNumber, 0, TotalWaves);
        waveText.text = $"Wave {current}/{TotalWaves}";
    }
}