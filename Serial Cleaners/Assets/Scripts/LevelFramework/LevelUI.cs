using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour
{
    // UI element references.
    [SerializeField] private Text timeCountdownText;
    [SerializeField] private TMP_Text timeCountdownTxt;

    // Text strings.
    [SerializeField] private string counterTitle = "Time remaining: ";


    // METHODS
    public void UpdateTimerText(float value)
    {
        System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(value);
        timeCountdownTxt.text = counterTitle +  string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
