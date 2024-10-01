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
    private ScoreManager scoreManager;

    private float lastPipeXPosition = 0f; // Track the last pipe's x position

    void Start()
    {
        myRigidbody.gravityScale = gravityScale;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        gameStarted = false;

        gameOverUI.SetActive(false);
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

            // Check if the bird passes the last pipe
            if (transform.position.x > lastPipeXPosition)
            {
                lastPipeXPosition = transform.position.x; // Update last pipe position
                PipeSpawnScript.IncrementPipesPassed(); // Increment pipes passed
                scoreManager.AddScore(1); // Add score
                Debug.Log("Pipes Passed: " + PipeSpawnScript.pipesPassed);
            }
        }

        // Check if the bird goes out of screen boundaries
        if (transform.position.y > Camera.main.orthographicSize || transform.position.y < -Camera.main.orthographicSize)
        {
            GameOver();
        }
    }

    void Flap()
    {
        myRigidbody.velocity = new Vector2(0, flapStrength);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver = true;
        myRigidbody.velocity = Vector2.zero;
        gameOverUI.SetActive(true);
        // Add the final score based on pipes passed
        scoreManager.AddScore(PipeSpawnScript.pipesPassed); // Add the number of pipes passed to the score
        PipeSpawnScript.pipesPassed = 0; // Reset pipes passed for the next game
        scoreManager.ResetScore(); // Reset score for the next game
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
