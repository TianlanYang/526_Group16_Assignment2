using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player
    public float jumpForce = 2.0f; // Force of the jump
    private bool isGrounded; // Is the player on the ground?

    void Update()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        transform.Translate(movement * speed * Time.deltaTime);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false; // Player is now in the air
        }
    }

    // Check if the player collides with the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true; // Player is on the ground
        }
    }
}