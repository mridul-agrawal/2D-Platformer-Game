using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        RefreshScoreBoard();
    }

    public void RefreshScoreBoard()
    {
        scoreText.text = "Score: " + score;
    }


}
