using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMesh Pro UI element
    private int score = 0; // Player's score

    void Start()
    {
        UpdateScoreText(); // Initialize the score text
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    public void MinusScore(int amount)
    {
        score -= amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Update the displayed score
    }
}
