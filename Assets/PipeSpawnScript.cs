using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject[] pipePrefabs;
    public float initialSpawnRate = 2f;
    public float minSpawnRate = 1f;
    public float difficultyIncreaseRate = 0.05f;
    public float heightOffset = 0.5f; 
    public float fixedGapHeight = 2.5f; 
    public float spawnHeightVariation = 1.5f; 
    public Vector2 spawnPosition = new Vector2(8.29f, 0.44f); 

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
        GameObject pipePrefab = pipePrefabs[Random.Range(0, pipePrefabs.Length)];

      
        float bottomPipeY = Random.Range(spawnPosition.y - heightOffset, spawnPosition.y + heightOffset - fixedGapHeight - spawnHeightVariation);
        float topPipeY = bottomPipeY + fixedGapHeight;

    
        Instantiate(pipePrefab, new Vector3(spawnPosition.x, topPipeY, 0), Quaternion.identity);
        Instantiate(pipePrefab, new Vector3(spawnPosition.x, bottomPipeY, 0), Quaternion.identity);

       
        currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - difficultyIncreaseRate);
    }
}

