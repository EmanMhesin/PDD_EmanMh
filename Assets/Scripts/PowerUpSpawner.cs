using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;  // Reference to the PowerUp prefab
    public float spawnInterval = 10f;  // Time interval between spawns
    public Vector3 spawnArea;  // Area where the power-ups can spawn

    private void Start()
    {
        InvokeRepeating("SpawnPowerUp", 0f, spawnInterval);  // Start spawning power-ups
    }

    private void SpawnPowerUp()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
            1f,  // Adjust Y position as needed
            Random.Range(-spawnArea.z / 2, spawnArea.z / 2)
        );

        Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);  // Instantiate the power-up
    }
}
