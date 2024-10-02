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
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
