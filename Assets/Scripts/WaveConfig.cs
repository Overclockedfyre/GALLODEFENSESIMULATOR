using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TD/Wave Config", fileName = "WaveConfig")]
public class WaveConfig : ScriptableObject
{
    public List<Wave> waves = new List<Wave>();
}

[Serializable]
public class Wave
{
    public float preDelay = 3f;
    public float interGroupDelay = 0.5f;
    public List<SpawnGroup> groups = new List<SpawnGroup>();
}

[Serializable]
public class SpawnGroup
{
    public GameObject enemyPrefab;
    public int count = 10;
    public float spawnInterval = 0.5f;
}