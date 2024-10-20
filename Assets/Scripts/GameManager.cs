using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton instance

    public TextMeshProUGUI scoreText;  // Reference to the score text in the UI
    public TextMeshProUGUI timerText;  // Reference to the timer text in the UI
    public TextMeshProUGUI gameOverText;  // Reference to the game over text in the UI
    private int score = 0;  // Player's score, starts at 0
    private int correctAnswers = 0;  // Tracks the number of correct answers

    private float timeRemaining = 60f;  // Set initial time to 60 seconds
    public bool isGameOver = false;  // Flag to check if the game is over
    public Button restartButton;  // Reference to the Restart Button
    public ParticleSystem gameOverParticles;  // Reference to the Particle System
    public bool isTimerPaused = false;  // Flag to control timer pause



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
        restartButton.gameObject.SetActive(false);  // Hide Restart Button initially
    }

    void Update()
    {
        if (!isGameOver && !isTimerPaused)
        {
            // Decrease the time remaining
            timeRemaining -= Time.deltaTime;

            // Update the timer display
            UpdateTimerUI();

            // If time runs out, end the game
            if (timeRemaining <= 0)
            {   
                Debug.Log("Time has run out!");
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

    
    public void PauseTimer(float duration)
{
    StartCoroutine(PauseTimerCoroutine(duration));
}

    private IEnumerator PauseTimerCoroutine(float duration)
{
    float originalTimeRemaining = timeRemaining;  // Store the original time remaining
    isTimerPaused = true;  // Pause only the timer

    yield return new WaitForSeconds(duration);  // Wait for the pause duration

    isTimerPaused = false;  // Resume only the timer
    timeRemaining = originalTimeRemaining;  // Restore the original time remaining
}



private void ResetGame()
    {
        score = 0;  // Initialize score to 0
        correctAnswers = 0;  // Initialize correct answers
        timeRemaining = 60f;  // Reset timer
        isGameOver = false;  // Game not over initially

        UpdateScoreUI();  // Update the score display
        UpdateTimerUI();  // Update the timer display
        gameOverText.gameObject.SetActive(false);  // Hide Game Over text
        restartButton.gameObject.SetActive(false);  // Hide Restart Button
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");
        ResetGame();  // Call the reset method
        Destroy(gameObject); //-------------------------
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
    }


 private void EndGame()
    {
        if (isGameOver) return; // Prevent re-entrance if already game over

        isGameOver = true;  // Set the game over flag to true
        timeRemaining = 0;  // Clamp the timer to 0

        // Display the Game Over message and correct answers count
        gameOverText.text = "Game Over!\nYou answered " + correctAnswers + " questions correctly!";
        gameOverText.gameObject.SetActive(true);  // Show Game Over text

        if (gameOverParticles != null)
    {
        gameOverParticles.transform.position = Camera.main.transform.position; // Position it at the camera for visibility
        gameOverParticles.Play();  // Play particle effect
    }

        restartButton.gameObject.SetActive(true);  // Show Restart Button
        restartButton.onClick.AddListener(RestartGame);  // Add listener to the Restart button

        Debug.Log("Game Over! Final Score: " + score);
    }

    public int GetScore()
{
    return score;  // Return the current score
}

}