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
        // Instantiate the pipe from the prefab
        GameObject pipe = Instantiate(pipePrefab, Vector3.zero, Quaternion.identity);

        // Ensure the spawned pipe has the "Pipe" tag
        pipe.tag = "Pipe";

        // Add or get a BoxCollider2D component
        BoxCollider2D pipeCollider = pipe.GetComponent<BoxCollider2D>();
        if (pipeCollider == null)
        {
            pipeCollider = pipe.AddComponent<BoxCollider2D>();
        }

        // Ensure the collider is set as a trigger
        pipeCollider.isTrigger = true;

        // Set the position of the pipe (either top or bottom)
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

        // Adjust spawn rate to increase difficulty
        currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - difficultyIncreaseRate);
    }
}

