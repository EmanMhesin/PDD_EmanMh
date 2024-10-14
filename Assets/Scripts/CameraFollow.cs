using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Offset from the player to the camera (adjust in Inspector)
    public float followSpeed = 10f; // Speed at which the camera follows the player

    private void LateUpdate()
    {
        // Calculate the new camera position based on the player's position and the offset
        Vector3 targetPosition = player.position + offset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        // Make sure the camera is always looking at the player
        transform.LookAt(player);
    }
}
