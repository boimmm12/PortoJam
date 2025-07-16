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

    private Vector2[] laneOffsets = new Vector2[3]
    {
        new Vector2(1.25f, 2f),   // Atas
        new Vector2(0f, 0f),      // Tengah
        new Vector2(-1.25f, -2f)  // Bawah
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

        activeSpawns.RemoveAll(obj => obj == null);
    }

    void TrySpawn(GameObject prefab)
    {
        int laneIndex = Random.Range(0, laneOffsets.Length);
        Vector2 spawnPos = (Vector2)transform.position + laneOffsets[laneIndex];

        // Cek semua spawn aktif
        foreach (GameObject obj in activeSpawns)
        {
            if (obj == null) continue;

            if (Mathf.Abs(spawnPos.x - obj.transform.position.x) < minXDistance)
            {
                return; // terlalu dekat, batalkan spawn
            }
        }

        GameObject spawned = Instantiate(prefab, spawnPos, Quaternion.identity);
        activeSpawns.Add(spawned);
    }
}
