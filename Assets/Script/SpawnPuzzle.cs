using UnityEngine;
using System.Collections.Generic;

public class SpawnPuzzle : MonoBehaviour
{
    public GameObject puzzleTriggerPrefab;
    public float timeBetweenSpawn = 0.6f; // Lebih jarang dari obstacle
    public float minXDistance = 4f;

    private float spawnTimer;

    // Sama seperti obstacle
    private Vector2[] laneOffsets = new Vector2[3]
    {
        new Vector2(1f, 2f),   // Atas
        new Vector2(0f, 0f),   // Tengah
        new Vector2(-1f, -2f)  // Bawah
    };

    private List<GameObject> activeTriggers = new List<GameObject>();

    void Update()
    {
        if (Time.time > spawnTimer)
        {
            TrySpawnPuzzle();
            spawnTimer = Time.time + timeBetweenSpawn;
        }

        activeTriggers.RemoveAll(obj => obj == null);
    }

    void TrySpawnPuzzle()
    {
        int laneIndex = Random.Range(0, laneOffsets.Length);
        Vector2 offset = laneOffsets[laneIndex];

        Vector2 spawnPos = (Vector2)transform.position + offset;

        foreach (GameObject obj in activeTriggers)
        {
            if (obj == null) continue;

            float existingX = obj.transform.position.x;
            if (Mathf.Abs(spawnPos.x - existingX) < minXDistance)
            {
                return;
            }
        }

        GameObject newTrigger = Instantiate(puzzleTriggerPrefab, spawnPos, Quaternion.identity);
        activeTriggers.Add(newTrigger);
    }
}
