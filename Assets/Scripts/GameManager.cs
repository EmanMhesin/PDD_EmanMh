using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton instance

    public TextMeshProUGUI scoreText;  // Reference to the score text in the UI
    public TextMeshProUGUI timerText;  // Reference to the timer text in the UI
    public TextMeshProUGUI gameOverText;  // Reference to the game over text in the UI
    private int score = 0;  // Player's score, starts at 0
    private int correctAnswers = 0;  // Tracks the number of correct answers

    private float timeRemaining = 60f;  // Set initial time to 60 seconds
    private bool isGameOver = false;  // Flag to check if the game is over

    void Awake()
    {
        // Implementing Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep the GameManager between scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate GameManagers
        }
    }

    void Start()
    {
        UpdateScoreUI();  // Initialize the score text at the start
        UpdateTimerUI();  // Initialize the timer text at the start
        gameOverText.gameObject.SetActive(false);  // Hide Game Over text initially
    }

    void Update()
    {
        if (!isGameOver)
        {
            // Decrease the time remaining
            timeRemaining -= Time.deltaTime;

            // Update the timer display
            UpdateTimerUI();

            // If time runs out, end the game
            if (timeRemaining <= 0)
            {
                EndGame();
            }
        }
    }

    // Method to update the score
    public void UpdateScore(int scoreDelta, bool isCorrect)
    {
        if (!isGameOver)
        {
            score += scoreDelta;  // Add or subtract from the score
            UpdateScoreUI();  // Update the UI text to reflect the new score

            if (isCorrect)  // Check if the answer was correct
            {
                correctAnswers++;  // Increment the number of correct answers
            }
        }
    }

    // Method to update the Score UI
    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();  // Update the score display
    }

    // Method to update the Timer UI
    private void UpdateTimerUI()
    {
        // Display the remaining time as an integer
        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();
    }

    // Method to end the game when time runs out
    private void EndGame()
    {
        isGameOver = true;  // Set the game over flag to true
        timeRemaining = 0;  // Clamp the timer to 0

        // Display the Game Over message and correct answers count
        gameOverText.text = "Game Over!\nYou answered " + correctAnswers + " questions correctly!";
        gameOverText.gameObject.SetActive(true);  // Show Game Over text

        Debug.Log("Game Over! Final Score: " + score);
    }
}
