using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SphereInteraction : MonoBehaviour
{
    public Canvas questionCanvas;  // Reference to the Canvas
    public string mathQuestion = "2 + 2 = ?";  // Default math question
    public string correctAnswer = "4";  // Correct answer for the math question

    private bool isGrabbed = false;  // Flag to check if the sphere is grabbed
    private TextMeshProUGUI questionText;  // Reference to the TextMeshPro text component for the question
    private TMP_InputField answerInputField;  // Reference to the TMP Input Field
    private Button submitButton;  // Reference to the submit button
    private TextMeshProUGUI feedbackText;  // Reference to the feedback text
    private MeshRenderer sphereRenderer;  // Reference to the sphere's MeshRenderer

    void Start()
    {
        // Find the TextMeshPro components in the child objects of the Canvas
        questionText = questionCanvas.GetComponentInChildren<TextMeshProUGUI>();
        answerInputField = questionCanvas.GetComponentInChildren<TMP_InputField>();
        submitButton = questionCanvas.GetComponentInChildren<Button>();

        // Get the MeshRenderer of the sphere
        sphereRenderer = GetComponent<MeshRenderer>();

        // Hide the canvas at the start
        questionCanvas.gameObject.SetActive(false);

        // Create and set up feedback text at the start
        feedbackText = CreateFeedbackText();

        // Add a listener to the submit button
        submitButton.onClick.AddListener(OnSubmitAnswer);
    }

    void Update()
    {
        if (isGrabbed)
        {
            // Show canvas and set the question text when the sphere is grabbed
            questionCanvas.gameObject.SetActive(true);
            questionText.text = mathQuestion;

            // Make the sphere invisible (disable the MeshRenderer)
            sphereRenderer.enabled = false;
        }
        else
        {
            // Hide canvas when not grabbed
            questionCanvas.gameObject.SetActive(false);

            // Optionally, re-enable the sphere renderer when the canvas is hidden
            // You can choose to do this if you want the sphere to reappear after releasing
            // sphereRenderer.enabled = true;
        }
    }

    public void GrabSphere()
    {
        isGrabbed = true;
    }

    public void ReleaseSphere()
    {
        isGrabbed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GrabSphere();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReleaseSphere();
        }
    }

    // This method is called when the player clicks the submit button
   
public void OnSubmitAnswer()
{
    string playerAnswer = answerInputField.text;  // Get the answer from the input field

    // Check if the player's answer is correct
    if (playerAnswer == correctAnswer)
    {
        feedbackText.text = "Correct!";  // Provide positive feedback
        feedbackText.color = Color.green;  // Set color to green for correct answer

        // Call UpdateScore with a +5 score and pass true for a correct answer
        GameManager.instance.UpdateScore(5, true);
    }
    else
    {
        feedbackText.text = "Incorrect, try again.";  // Provide negative feedback
        feedbackText.color = Color.red;  // Set color to red for incorrect answer

        // Call UpdateScore with a -5 score and pass false for an incorrect answer
        GameManager.instance.UpdateScore(-5, false);
    }

    feedbackText.gameObject.SetActive(true);  // Show the feedback text
}



    // Method to create feedback text dynamically in the Canvas
    TextMeshProUGUI CreateFeedbackText()
    {
        // Create a new GameObject for feedback text
        GameObject feedbackObject = new GameObject("FeedbackText");
        feedbackObject.transform.SetParent(questionCanvas.transform, false);  // Attach it to the Canvas

        // Add TextMeshPro component
        TextMeshProUGUI feedbackTMP = feedbackObject.AddComponent<TextMeshProUGUI>();

        // Customize the text properties
        feedbackTMP.fontSize = 36;  // Set the font size
        feedbackTMP.alignment = TextAlignmentOptions.Center;  // Center the text
        feedbackTMP.text = "";  // Empty text initially

        // Position the feedback text within the Canvas (below input field)
        RectTransform rectTransform = feedbackTMP.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(400, 100);
        rectTransform.anchoredPosition = new Vector2(0, -100);  // Adjust position as necessary

        feedbackTMP.gameObject.SetActive(false);  // Hide the feedback initially

        return feedbackTMP;
    }
}
