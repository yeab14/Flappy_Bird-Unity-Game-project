using UnityEngine;
using UnityEngine.SceneManagement;

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
    public ParticleSystem crashEffect; // Particle effect for pipe collision
    public Camera mainCamera;

    private float originalTimeScale = 1f;
    private float slowMotionFactor = 0.2f;
    private float slowMotionDuration = 1f;
    private float cameraShakeAmount = 0.1f;

    private ScoreManager scoreManager; // Reference to ScoreManager

    void Start()
    {
        myRigidbody.gravityScale = gravityScale;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        gameStarted = false;

        gameOverUI.SetActive(false);

        // Find the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
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
        if (eatEffect != null)
        {
            Instantiate(eatEffect, fruit.transform.position, Quaternion.identity);
        }

        // Notify the ScoreManager that a fruit has been eaten
        if (scoreManager != null)
        {
            scoreManager.AddScore(1); // Assuming each fruit is worth 1 point
        }

        StartCoroutine(ShrinkAndDestroyFruit(fruit));
        Debug.Log("Bird ate the fruit!");
    }

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

    // Detect if bird touches the pipe
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Pipe"))
        {
            // The bird has touched the pipe; trigger game over with effects
            if (crashEffect != null)
            {
                Instantiate(crashEffect, transform.position, Quaternion.identity);
            }
            StartCoroutine(HandleSlowMotion());
            StartCoroutine(CameraShake());
            GameOver();
        }
    }

    // Slow-motion effect coroutine
    System.Collections.IEnumerator HandleSlowMotion()
    {
        Time.timeScale = slowMotionFactor;
        yield return new WaitForSecondsRealtime(slowMotionDuration);
        Time.timeScale = originalTimeScale;
    }

    // Camera shake effect coroutine
    System.Collections.IEnumerator CameraShake()
    {
        Vector3 originalPos = mainCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < slowMotionDuration)
        {
            float xOffset = Random.Range(-cameraShakeAmount, cameraShakeAmount);
            float yOffset = Random.Range(-cameraShakeAmount, cameraShakeAmount);

            mainCamera.transform.position = new Vector3(originalPos.x + xOffset, originalPos.y + yOffset, originalPos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPos;
    }

    void GameOver()
    {
        gameOver = true;
        myRigidbody.velocity = Vector2.zero;
        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
