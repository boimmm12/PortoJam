using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [Header("Obstacle Settings")]
    public GameObject obstaclePrefab;
    public float obstacleSpawnRate = 1.5f;

    [Header("Puzzle Settings")]
    public GameObject puzzleTriggerPrefab;
    public float puzzleSpawnRate = 4f;

    [Header("General")]
    public float minXDistance = 3f;

    private float nextObstacleTime = 0f;
    private float nextPuzzleTime = 0f;

    [Header("Coin Settings")]
    public GameObject coinPrefab;
    public float coinSpawnRate = 2f;
    public int maxCoinsPerGroup = 5;
    public float coinSpacing = 1.2f;
    private float nextCoinTime = 0f;

    private Vector2[] laneOffsets = new Vector2[3]
    {
        new Vector2(1.7f, 1.3f),   // Atas
        new Vector2(0f, 0f),      // Tengah
        new Vector2(-0.9f, -1.1f)  // Bawah
    };

    private List<GameObject> activeSpawns = new List<GameObject>();

    void Update()
    {
        if (Time.time >= nextObstacleTime)
        {
            TrySpawn(obstaclePrefab);
            nextObstacleTime = Time.time + obstacleSpawnRate;
        }

        if (Time.time >= nextPuzzleTime)
        {
            TrySpawn(puzzleTriggerPrefab);
            nextPuzzleTime = Time.time + puzzleSpawnRate;
        }

        if (Time.time >= nextCoinTime)
        {
            SpawnCoinGroup();
            nextCoinTime = Time.time + coinSpawnRate;
        }

        activeSpawns.RemoveAll(obj => obj == null);
    }

    void SpawnCoinGroup()
    {
        int laneIndex = Random.Range(0, laneOffsets.Length);
        Vector2 laneOffset = laneOffsets[laneIndex];

        int coinCount = Random.Range(1, maxCoinsPerGroup + 1);
        Vector2 startPos = (Vector2)transform.position + laneOffset;

        for (int i = 0; i < coinCount; i++)
        {
            Vector2 coinPos = startPos + Vector2.right * (coinSpacing * i);
            GameObject coin = Instantiate(coinPrefab, coinPos, Quaternion.identity);
            activeSpawns.Add(coin);
        }
    }

    void TrySpawn(GameObject prefab)
    {
        int laneIndex = Random.Range(0, laneOffsets.Length);
        Vector2 spawnPos = (Vector2)transform.position + laneOffsets[laneIndex];

        foreach (GameObject obj in activeSpawns)
        {
            if (obj == null) continue;

            if (Mathf.Abs(spawnPos.x - obj.transform.position.x) < minXDistance)
            {
                return;
            }
        }

        GameObject spawned = Instantiate(prefab, spawnPos, Quaternion.identity);
        activeSpawns.Add(spawned);
    }
}
