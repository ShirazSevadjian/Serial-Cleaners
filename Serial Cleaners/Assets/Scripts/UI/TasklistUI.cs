using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasklistUI : MonoBehaviour
{
    // List of tasks.
    public Dictionary<TaskManager, TMPro.TMP_Text> textPerTask;

    // Visual params.
    // Basic label prefab
    // Each task label moves down by.
    // current move down.
    public float completedTaskOpacity = 0.75f;


    // create label for each task

    public void CreateLabelForTask(TaskManager myTaskManager)
    { 

    }



    // Update the relevant task label.
    public void UpdateTaskLabel(TaskManager myTaskManager)
    {
        textPerTask[myTaskManager].text = string.Format("{0}: {1}/{2}", myTaskManager.taskReference.taskText, myTaskManager.DoneXTimesSoFar, myTaskManager.DoXTimesInTotal);

        // If the task is completed, dull its text.
        if (myTaskManager.IsTaskDone)
            textPerTask[myTaskManager].alpha = completedTaskOpacity;
    }
}
