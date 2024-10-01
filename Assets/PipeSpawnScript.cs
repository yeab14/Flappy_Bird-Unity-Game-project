using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipePrefab;
    public float initialSpawnRate = 2f;
    public float minSpawnRate = 1f;
    public float difficultyIncreaseRate = 0.05f;
    public float fixedGapHeight = 2.5f;
    public Vector2 spawnPosition = new Vector2(8.29f, 0.44f);
    public float heightOffset = 1.0f;

    private float timer = 0f;
    private float currentSpawnRate;

    // Static variable to track pipes passed
    public static int pipesPassed = 0;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= currentSpawnRate)
        {
            SpawnPipe();
            timer = 0f;
        }
    }

    void SpawnPipe()
    {
        GameObject pipe = Instantiate(pipePrefab, Vector3.zero, Quaternion.identity);

        // Determine the Y position for the pipes and instantiate them
        if (Random.Range(0, 2) == 0)
        {
            float topPipeY = spawnPosition.y + (fixedGapHeight / 2) + Random.Range(-heightOffset, heightOffset);
            pipe.transform.position = new Vector3(spawnPosition.x, topPipeY, 0);
        }
        else
        {
            float bottomPipeY = spawnPosition.y - (fixedGapHeight / 2) - Random.Range(-heightOffset, heightOffset);
            pipe.transform.position = new Vector3(spawnPosition.x, bottomPipeY, 0);
        }

        currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - difficultyIncreaseRate);
    }

    // Method to increment pipes passed
    public static void IncrementPipesPassed()
    {
        pipesPassed++;
    }
}
