using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Task_", menuName = "ScriptableObjects/Task Information", order = 2)]
public class Task : ScriptableObject
{
    // Task info.
    public string taskText; // The task's in-game description.
    public Sprite taskIcon; // Icon representing the task. Might be used in the UI.

    // Reference to the task's manager class.
    // Have it as a prefab in its own holder instead?
    public string taskManagerClassName;
}