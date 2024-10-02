using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleJumpController : MonoBehaviour
{
    private bool playerCanJump = false;

    // Detect when the player stands on the obstacle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Allow the player to jump once when they land on this obstacle
            PlayerController playerController = collision.collider.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerCanJump = true;
                playerController.SetJumpAllowed(true); // Allow a jump
            }
        }
    }

    // Detect when the player leaves the obstacle
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerController playerController = collision.collider.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetJumpAllowed(false); // Revoke the ability to jump again from this obstacle
            }
            playerCanJump = false;
        }
    }
}
