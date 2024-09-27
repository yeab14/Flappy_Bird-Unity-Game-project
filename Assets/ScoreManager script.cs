using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; 
    private int score; 

    void Start()
    {
        score = 0; 
        UpdateScoreText(); 
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; 
    }

    public void IncreaseScore()
    {
        score++; 
        UpdateScoreText(); 
    }
}

