using UnityEngine;
using UnityEngine.UI; 


public class ScoreManager : MonoBehaviour
{
    public Text scoreText; 
    private int score = 0;

    void Start()
    {
        UpdateScoreText();
    }

    public void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); 
    }
}
