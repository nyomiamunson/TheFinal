using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jump = 5f;
    public float rotationSpeed = 180f; // Adjust this value to control the rotation speed

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

    void FixedUpdate()
    {
        // Move the player to the right automatically
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);

        // Rotate the player based on the movement direction
        if (transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -rotationSpeed * Time.fixedDeltaTime);
        }

        // Update camera position to follow the player
        if (mainCamera != null)
            mainCamera.transform.position = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Add a jump force to the Rigidbody2D component
            rb.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);

            // Trigger the jump annimation
            animator.SetTrigger("Jump");
        }
    }

    public void ChangeThroughPortal(float newSpeed, bool isShipMode, float gravityScale)
    {
        // Update the player's speed
        speed = newSpeed;

        // Update the player's gamemode or sprite
        // Implement your logic here based on the isShipMode value

        // Update the gravity scale
        rb.gravityScale = gravityScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged
        if (collision.gameObject.CompareTag("Spike")){
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
