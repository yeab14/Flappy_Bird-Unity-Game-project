using UnityEngine;
using UnityEngine.UI;  // For UI elements
using TMPro;  // For TextMeshPro support

public class ScoreManager : MonoBehaviour
{
    // Allow assignment of either Unity's Text or TextMeshPro
    public Text scoreText;             // For Unity UI Text
    public TextMeshProUGUI scoreTextTMP; // For TextMeshPro UI Text
    public int score = 0;              // Current score
    public int highScore = 0;          // High score tracking
    public int scoreMultiplier = 1;    // Multiplier for score (can be adjusted)

    void Start()
    {
        // Initialize the score display
        UpdateScoreText();
    }

    // Call this method to increase the score
    public void AddScore(int amount)
    {
        score += amount * scoreMultiplier;  // Increase score by the amount multiplied by multiplier
        if (score > highScore)               // Check if current score exceeds high score
        {
            highScore = score;               // Update high score
            Debug.Log("New High Score: " + highScore);
        }
        UpdateScoreText();                   // Update the UI text
    }

    // Method to update the score text UI
    private void UpdateScoreText()
    {
        // Update the appropriate score text based on what's assigned
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;  // Update the score display for Unity UI Text
        }
        if (scoreTextTMP != null)
        {
            scoreTextTMP.text = "Score: " + score;  // Update the score display for TextMeshPro
        }
    }

    // Method to reset the score when restarting the game
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();  // Reset score display
    }
}
