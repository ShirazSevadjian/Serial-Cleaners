using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    // UI element references.
    [SerializeField] private Text timeCountdownText;

    // Text strings.
    [SerializeField] private string counterTitle = "Time remaining: ";


    // METHODS
    public void UpdateTimerText(float value)
    {
        timeCountdownText.text = counterTitle + value.ToString("000");
    }
}
