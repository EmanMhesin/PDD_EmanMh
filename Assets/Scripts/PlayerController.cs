using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float turnSpeed = 300f; // Rotation speed
    public float jumpForce = 5f; // Force applied to the player when jumping
    public LayerMask groundLayer; // Layer mask to identify the ground
    
    private Rigidbody rb; // Rigidbody component for physics-based movement
    private bool isGrounded; // To check if the player is on the ground

    private void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Handle movement and rotation
        Move();
        Rotate();

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void Move()
    {
        // Get input for horizontal (A/D, Left/Right arrows) and vertical (W/S, Up/Down arrows) movement
        float moveX = Input.GetAxis("Horizontal"); // Left and right movement
        float moveZ = Input.GetAxis("Vertical"); // Forward and backward movement

        // Calculate the direction the player will move based on the input
        Vector3 moveDirection = transform.forward * moveZ + transform.right * moveX;

        // Move the player by applying force in the direction of movement
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        // Get mouse input for rotating the character
        float mouseX = Input.GetAxis("Mouse X");

        // Rotate the player based on the mouse input
        transform.Rotate(0, mouseX * turnSpeed * Time.deltaTime, 0);
    }

    private void Jump()
    {
        // Apply an upward force to make the player jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        // Check if the player is grounded by detecting collisions with objects tagged as "Ground"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Set isGrounded to false when the player is no longer colliding with the ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
