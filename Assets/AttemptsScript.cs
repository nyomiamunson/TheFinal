using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttemptsScript : MonoBehaviour
{
    public TextMeshProUGUI attemptsText; // Reference to the TextMeshProUGUI component
    public int attemptsValue = 0;

    private void Start()
    {
        UpdateAttemptsText();
    }

    public void Attempts()
    {
        attemptsValue++;
        UpdateAttemptsText();
    }

    void UpdateAttemptsText()
    {
        if (attemptsText != null)
        {
            attemptsText.text = "Attempts: " + attemptsValue;
        }
    }
}