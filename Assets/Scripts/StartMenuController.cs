using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    public GameObject startMenuCanvas;  // Reference to the Start Menu Canvas
    public Button playButton;           // Reference to the Play Button

    void Start()
    {
        // Add listener to the Play button
        playButton.onClick.AddListener(StartGame);

        // Initially show the start menu and stop the game
        ShowStartMenu();
    }

    void ShowStartMenu()
    {
        // Display the start menu and freeze time
        startMenuCanvas.SetActive(true);
        Time.timeScale = 0f;  // Pause the game
    }

    public void StartGame()
    {
        // Hide the start menu and resume time
        startMenuCanvas.SetActive(false);
        Time.timeScale = 1f;  // Resume the game
    }
}
