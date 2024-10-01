using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorSwapper : MonoBehaviour
{
    public GameObject sprite1; // Assign the first background sprite in the Inspector
    public GameObject sprite2; // Assign the second background sprite in the Inspector
    public GameObject[] blackObstacles; // Assign black obstacles (squares) in Inspector
    public GameObject[] whiteObstacles; // Assign white obstacles (squares) in Inspector

    private SpriteRenderer spriteRenderer1;
    private SpriteRenderer spriteRenderer2;

    void Start()
    {
        // Get the SpriteRenderer components of each GameObject
        if (sprite1 != null && sprite2 != null)
        {
            spriteRenderer1 = sprite1.GetComponent<SpriteRenderer>();
            spriteRenderer2 = sprite2.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogError("Please assign both sprites to the script in the Inspector.");
        }

        UpdateObstacleColliders();
    }

    void Update()
    {
        // Check if the "C" key is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwapColors();
        }
    }

    void SwapColors()
    {
        if (spriteRenderer1 != null && spriteRenderer2 != null)
        {
            // Swap the colors of the two sprites
            Color tempColor = spriteRenderer1.color;
            spriteRenderer1.color = spriteRenderer2.color;
            spriteRenderer2.color = tempColor;

            // Update the colliders of the black obstacles based on the new background color
            UpdateObstacleColliders();
        }
    }

    void UpdateObstacleColliders()
    {
        // If either background color is black, we disable the colliders on black obstacles
        bool isBackgroundBlack = (spriteRenderer1.color == Color.black);

        foreach (GameObject obstacle in blackObstacles)
        {
            Collider2D collider = obstacle.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = !isBackgroundBlack; // Disable colliders if the background is black, enable if it is not
            }
        }

        foreach (GameObject obstacle in whiteObstacles)
        {
            Collider2D collider = obstacle.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = isBackgroundBlack; // Disable colliders if the background is black, enable if it is not
            }
        }
    }

    // Method to check if the current background is black
    public bool IsBackgroundBlack()
    {
        if (spriteRenderer1 != null)
        {
            return spriteRenderer1.color == Color.black;
        }
        return false;
    }
}
