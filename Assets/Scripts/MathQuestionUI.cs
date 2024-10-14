using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this for TextMeshPro support

public class MathQuestionUI : MonoBehaviour
{
    public TextMeshProUGUI questionText;  // The text field for the question
    public Button[] answerButtons;        // The buttons for answers
    public GameObject uiPanel;            // The math question UI panel

    private int correctAnswerIndex;       // Index of the correct answer

    // Reference to the GameManager for the score
    private GameManager gameManager;

    void Start()
    {
        // Make sure the UI panel is hidden at the start
        uiPanel.SetActive(false);

        // Reference to the GameManager for scoring
        gameManager = FindObjectOfType<GameManager>();
    }

    // This function shows the UI when the player triggers the event (like walking on the golden sphere)
    public void ShowRandomQuestion()
    {
        // Set a random question
        SetRandomQuestion();

        // Show the question panel
        uiPanel.SetActive(true);
    }

    private void SetRandomQuestion()
    {
        // Randomly choose a question
        int randomIndex = Random.Range(0, questions.Length);
        SetQuestion(questions[randomIndex], new string[] {
            answers[randomIndex, 0],
            answers[randomIndex, 1],
            answers[randomIndex, 2]
        }, correctIndices[randomIndex]);
    }

    private void SetQuestion(string question, string[] answers, int correctIndex)
    {
        // Set the question text and correct answer index
        questionText.text = question;
        correctAnswerIndex = correctIndex;

        // Set the text for each button
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];
            answerButtons[i].onClick.RemoveAllListeners();  // Clear previous listeners

            int buttonIndex = i;  // Capture the index in a local variable
            answerButtons[i].onClick.AddListener(() => CheckAnswer(buttonIndex));  // Add new listener for click
        }
    }

    private void CheckAnswer(int index)
    {
        if (index == correctAnswerIndex)
        {
            Debug.Log("Correct Answer!");

            // Add points to the player's score
            gameManager.AddScore(10);

            // Hide the panel after the correct answer
            uiPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Wrong Answer! Try again.");
        }
    }

    // Question data (you've already added these in Step 6)
    private string[] questions = { "What is 5 + 3?", "What is 7 - 2?", "What is 6 * 2?" };
    private string[,] answers = { { "6", "8", "9" }, { "6", "5", "4" }, { "12", "11", "10" } };
    private int[] correctIndices = { 1, 1, 0 };  // Correct answers indices for each question
}
