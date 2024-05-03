using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the Text Mesh Pro namespace
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 5f;

    Rigidbody2D rb;
    Animator animator;

    // Reference to the camera
    public Camera mainCamera;

    // Text to display attempts
    public TextMeshProUGUI attemptsText; // Reference to the Text Mesh Pro Text element
    private int attempts = 0; // Counter for attempts

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Load previous attempts from PlayerPrefs
        attempts = PlayerPrefs.GetInt("Attempts", 0);

        // Check if a camera is assigned
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // Update attempts text
        UpdateAttemptsText();
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
            // Increment attempts
            attempts++;

            // Save attempts to PlayerPrefs
            PlayerPrefs.SetInt("Attempts", attempts);

            // Update attempts text
            UpdateAttemptsText();

            // Restart the game
            RestartGame();
        }
    }

    private void UpdateAttemptsText()
    {
        // Update the Text Mesh Pro Text element with the current number of attempts
        attemptsText.text = "Attempts: " + attempts;
    }

    void RestartGame()
    {
        // You can reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
