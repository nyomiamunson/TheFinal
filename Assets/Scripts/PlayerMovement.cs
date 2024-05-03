using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 5f;

    Rigidbody2D rb;
    Animator animator;

    // Reference to the camera
    public Camera mainCamera;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Check if a camera is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void FixedUpdate()
    {
        // Move the player to the right automatically
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);

        // Update camera position to follow the player
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Add a jump force to the Rigidbody2D component
            rb.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);

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
