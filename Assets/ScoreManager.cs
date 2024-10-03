using UnityEngine;
using UnityEngine.UI; // For UI elements
using TMPro; // For TextMeshPro support

public class ScoreManager : MonoBehaviour
{
    // References to the UI text components
    public Text scoreText; // For Unity's UI Text
    public TextMeshProUGUI scoreTextTMP; // For TextMeshPro UI Text

    // Score variables
    private int score = 0; // Current score
    public int highScore = 0; // High score tracking
    private const string HighScoreKey = "HighScore"; // Key for PlayerPrefs

    void Start()
    {
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        UpdateScoreText();
    }

    // Method to add score based on fruits eaten
    public void AddScore(int amount)
    {
        score += amount; // Increment the score by the amount passed (1 in this case)
        CheckHighScore(); // Check if we have a new high score
        UpdateScoreText(); // Update the UI text
    }

    // Method to check and update the high score
    private void CheckHighScore()
    {
        if (score > highScore)
        {
            highScore = score; // Update high score
            PlayerPrefs.SetInt(HighScoreKey, highScore); // Save it to PlayerPrefs
            PlayerPrefs.Save(); // Ensure it is saved
            Debug.Log("New High Score: " + highScore); // Debug log for high score
        }
    }

    // Method to update the score display
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Update score text
        }
        if (scoreTextTMP != null)
        {
            scoreTextTMP.text = "Score: " + score; // Update score text for TextMeshPro
        }
    }

    // Method to reset the score when restarting the game
    public void ResetScore()
    {
        score = 0; // Reset score to 0
        UpdateScoreText(); // Update display
    }

    // Optional: Display high score (if needed)
    public void DisplayHighScore(Text highScoreText)
    {
        highScoreText.text = "High Score: " + highScore; // Update high score display
    }
}
