using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;

    private Rigidbody2D rb;
    private Animator animator;

    public Camera mainCamera; // Reference to the camera

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Check if a camera is assigned
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        // Move the player to the right automatically
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Update camera position to follow the player
        if (mainCamera != null)
            mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Add a jump force to the Rigidbody2D component
            rb.velocity = new Vector2(rb.velocity.x, jump);

            // Trigger the jump animation
            animator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged
        if (collision.gameObject.CompareTag("Spike"))
        {
            // Restart the game
            RestartGame();
        }
    }

    void RestartGame()
    {
        // You can reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}