using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f; 
    public float jumpForce = 2.0f; 
    private bool isGrounded; 
    private bool canJumpFromObstacle;
    private Rigidbody2D rb;
    private Vector3 originalPosition;
    public BackgroundColorSwapper colorSwapScript; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;

        if (colorSwapScript == null)
        {
            colorSwapScript = FindObjectOfType<BackgroundColorSwapper>();
        }
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        transform.Translate(movement * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || canJumpFromObstacle))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            if (canJumpFromObstacle && !isGrounded)
            {
                canJumpFromObstacle = false;
            }
            isGrounded = false; 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Whitetrap")
        {
            if (colorSwapScript != null && colorSwapScript.IsBackgroundBlack())
            {
                Debug.Log("Position have been reset!!!!!!");
                ResetPosition(); 
            }
        }

        if (collision.gameObject.tag == "Blacktrap")
        {
            if (colorSwapScript != null && !colorSwapScript.IsBackgroundBlack())
            {
                Debug.Log("Position have been reset!!!!!!");
                ResetPosition(); 
            }
        }

        if (collision.gameObject.tag == "RedFlag")
        {
            speed = 0f;
            rb.velocity = Vector2.zero;  
            LoadNextLevel(); 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Whitetrap")
        {
            if (colorSwapScript != null && colorSwapScript.IsBackgroundBlack())
            {
                Debug.Log("Position have been reset!!!!!!");
                ResetPosition(); 
            }
        }

        if (collision.gameObject.tag == "Blacktrap")
        {
            if (colorSwapScript != null && !colorSwapScript.IsBackgroundBlack())
            {
                Debug.Log("Position have been reset!!!!!!");
                ResetPosition(); 
            }
        }
    }

    private void ResetPosition()
    {
        rb.velocity = Vector2.zero;  
        transform.position = originalPosition;
    }

    public void SetJumpAllowed(bool allowed)
    {
        canJumpFromObstacle = allowed;
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Done");
        }
    }
}
