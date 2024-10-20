using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SphereInteraction : MonoBehaviour
{
    public Canvas questionCanvas;
    public string mathQuestion = "2 + 2 = ?";
    public string correctAnswer = "4";

    private bool isGrabbed = false;
    private TextMeshProUGUI questionText;
    private TMP_InputField answerInputField;
    private Button submitButton;
    private TextMeshProUGUI feedbackText;
    private MeshRenderer sphereRenderer;
    private AudioSource audioSource;

    // Add a reference to the particle effect prefab
    public ParticleSystem correctAnswerParticles;

    void Start()
    {
        questionText = questionCanvas.GetComponentInChildren<TextMeshProUGUI>();
        answerInputField = questionCanvas.GetComponentInChildren<TMP_InputField>();
        submitButton = questionCanvas.GetComponentInChildren<Button>();
        sphereRenderer = GetComponent<MeshRenderer>();
        questionCanvas.gameObject.SetActive(false);
        feedbackText = CreateFeedbackText();
        submitButton.onClick.AddListener(OnSubmitAnswer);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.instance.isGameOver && isGrabbed)
        {
            questionCanvas.gameObject.SetActive(true);
            questionText.text = mathQuestion;
            sphereRenderer.enabled = false;
        }
        else
        {
            questionCanvas.gameObject.SetActive(false);
        }
    }

    public void GrabSphere()
    {
        isGrabbed = true;
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void ReleaseSphere()
    {
        isGrabbed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!GameManager.instance.isGameOver && other.CompareTag("Player"))
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

    public void OnSubmitAnswer()
    {
        string playerAnswer = answerInputField.text;

        if (playerAnswer == correctAnswer)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;

            // Instantiate and play particle effect at the sphere's position
            if (correctAnswerParticles != null)
            {
                Instantiate(correctAnswerParticles, transform.position, Quaternion.identity);
            }

            GameManager.instance.UpdateScore(5, true);
            Debug.Log("Score updated. Current score: " + GameManager.instance.GetScore());
        }
        else
        {
            feedbackText.text = "Incorrect, try again.";
            feedbackText.color = Color.red;
            GameManager.instance.UpdateScore(-5, false);
            Debug.Log("Score updated. Current score: " + GameManager.instance.GetScore());
        }

        feedbackText.gameObject.SetActive(true);
    }

    TextMeshProUGUI CreateFeedbackText()
    {
        GameObject feedbackObject = new GameObject("FeedbackText");
        feedbackObject.transform.SetParent(questionCanvas.transform, false);
        TextMeshProUGUI feedbackTMP = feedbackObject.AddComponent<TextMeshProUGUI>();
        feedbackTMP.fontSize = 36;
        feedbackTMP.alignment = TextAlignmentOptions.Center;
        feedbackTMP.text = "";
        RectTransform rectTransform = feedbackTMP.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(400, 100);
        rectTransform.anchoredPosition = new Vector2(0, -100);
        feedbackTMP.gameObject.SetActive(false);
        return feedbackTMP;
    }
}


