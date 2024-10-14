using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MathProblem : MonoBehaviour
{
    public GameObject uiPanel; // Reference to the UI Panel for math questions
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Show the math question UI
            uiPanel.SetActive(true);
            // Disable the math problem object to prevent re-collecting
            gameObject.SetActive(false);
        }
    }
}
