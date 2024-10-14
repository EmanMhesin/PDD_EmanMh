using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;  // Player's score
    public TextMeshProUGUI scoreText;  // Text UI to show the score

    void Start()
    {
        UpdateScoreUI();
    }

    // This function will be called when the player answers correctly
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }
}
