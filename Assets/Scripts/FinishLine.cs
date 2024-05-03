using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public GameObject finishImage; // Reference to the UI Image GameObject

    // Start is called before the first frame update
    void Start()
    {
        // Initially disable the finish image
        finishImage.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Enable the finish image
            finishImage.SetActive(true);
        }
    }
}
