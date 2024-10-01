using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // For UI elements

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength = 5f;
    public float gravityScale = 1f;
    private bool gameStarted;
    public bool gameOver = false;

    public GameObject gameOverUI;  // Reference to the Game Over UI (Canvas)

    void Start()
    {
        myRigidbody.gravityScale = gravityScale;
        myRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        gameStarted = false;

        // Initially, hide the Game Over UI
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
                myRigidbody.velocity = Vector2.zero; // Prevent movement before the game starts
            }
        }

        // Check for out-of-bounds to trigger Game Over (optional)
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
        // Check for collision with pipes
        if (collision.gameObject.CompareTag("Pipe"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOver = true;
        myRigidbody.velocity = Vector2.zero;  // Stop the bird's movement

        // Show the Game Over UI
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);  // Activate the UI
        }
    }

    // Method to restart the game, linked to the Restart button
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Restart the current scene
    }
}
