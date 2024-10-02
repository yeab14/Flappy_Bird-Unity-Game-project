using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawnScript : MonoBehaviour
{
    public GameObject fruitPrefab;              // Reference to the fruit prefab
    public float initialSpawnRate = 2f;         // Initial spawn time for the fruits
    public float minSpawnRate = 1f;             // Minimum time between spawns
    public float difficultyIncreaseRate = 0.05f; // Difficulty increase rate for spawn
    public Vector2 spawnPosition = new Vector2(8.29f, 0.44f); // Spawn position for the fruits
    public float heightOffset = 1.0f;           // Height offset for random spawn positions

    private float timer = 0f;                   // Timer to track spawn rate
    private float currentSpawnRate;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;    // Set initial spawn rate
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= currentSpawnRate)
        {
            SpawnFruit();   // Spawn the fruit when the timer exceeds the spawn rate
            timer = 0f;     // Reset the timer
        }
    }

    void SpawnFruit()
    {
        GameObject fruit = Instantiate(fruitPrefab, Vector3.zero, Quaternion.identity);

        // Randomize the Y position of the fruit within the height offset range
        float randomY = spawnPosition.y + Random.Range(-heightOffset, heightOffset);
        fruit.transform.position = new Vector3(spawnPosition.x, randomY, 0);

        // Adjust the spawn rate for increased difficulty
        currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - difficultyIncreaseRate);
    }
}


