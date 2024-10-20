using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUp : MonoBehaviour
{
    public float pauseDuration = 5f;  // Duration to pause the timer
    private bool isActive = true;  // Flag to check if the power-up is still active

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isActive)  // Check if the player collides with the power-up
        {
            // Call the GameManager method to pause the timer
            GameManager.instance.PauseTimer(pauseDuration);
            isActive = false;  // Disable the power-up
            Destroy(gameObject);  // Destroy the power-up after collection
        }
    }
}

