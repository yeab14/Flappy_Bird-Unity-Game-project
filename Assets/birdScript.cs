using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength = 5f;
    public float gravityScale = 1f;
    private bool gameStarted;
    public bool gameOver = false;

    public GameObject gameOverUI;
    public float eatRadius = 2.0f; // Proximity radius for eating the fruit
    public ParticleSystem eatEffect; // Optional particle effect when fruit is eaten

    void Start()
    {
        myRigidbody.gravityScale = gravityScale;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        gameStarted = false;

        gameOverUI.SetActive(false);
    }

    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!gameStarted)
                {
                    gameStarted = true;
                }
                Flap();
            }

            if (!gameStarted)
            {
                myRigidbody.velocity = Vector2.zero;
            }
        }

        if (transform.position.y > Camera.main.orthographicSize || transform.position.y < -Camera.main.orthographicSize)
        {
            GameOver();
        }

        CheckForFruitProximity();
    }

    void Flap()
    {
        myRigidbody.velocity = new Vector2(0, flapStrength);
    }

    // Check if bird is close to any fruit
    void CheckForFruitProximity()
    {
        GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
        foreach (GameObject fruit in fruits)
        {
            float distanceToFruit = Vector2.Distance(transform.position, fruit.transform.position);
            if (distanceToFruit <= eatRadius)
            {
                EatFruit(fruit);
            }
        }
    }

    void EatFruit(GameObject fruit)
    {
        // Play an optional particle effect
        if (eatEffect != null)
        {
            Instantiate(eatEffect, fruit.transform.position, Quaternion.identity);
        }

        // Shrink the fruit before destroying it
        StartCoroutine(ShrinkAndDestroyFruit(fruit));

        Debug.Log("Bird ate the fruit!");
    }

    // Shrinking effect
    System.Collections.IEnumerator ShrinkAndDestroyFruit(GameObject fruit)
    {
        float shrinkDuration = 0.5f;
        float elapsedTime = 0f;

        Vector3 originalScale = fruit.transform.localScale;

        while (elapsedTime < shrinkDuration)
        {
            fruit.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(fruit);
    }

    void GameOver()
    {
        gameOver = true;
        myRigidbody.velocity = Vector2.zero;
        gameOverUI.SetActive(true);
    }
}

