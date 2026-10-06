/* PlayerController:
 *
 * Manages player movement and jumping. 
 * I am using character controller here to manage movement instead of rigidbody.
 */

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    private Vector3 playerVelocity;
    private CharacterController controller;
    private bool isGrounded;

    // Start is called once before the first execution of Update after the 
    // MonoBehaviour is created
    void Start()
    {

        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("No CharacterController found on player object!");
        }
    }

    // Update is called once per frame
    // The movements are in update function and it's fine because it's dependent
    // on delta time rather than frames.
    void Update()
    {
        //Boolean that records if the player is touching the ground
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            // Slight downward velocity to keep grounded stable
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate horizontal movement vector relative to local directions
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        //if player is grounded, space will jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //v0 = sqrt(-2gh) calculates initial velocity based on jump height
            playerVelocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
        }

        //gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // Move
        Vector3 finalMove = move * moveSpeed + Vector3.up * playerVelocity.y;
        controller.Move(finalMove * Time.deltaTime);
    }

}
