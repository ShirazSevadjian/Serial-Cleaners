using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasklistUI : MonoBehaviour
{
    // List of tasks.
    public Dictionary<TaskManager, TMPro.TMP_Text> textPerTask = new Dictionary<TaskManager, TMPro.TMP_Text>();

    // Visual parameters.
    public GameObject taskLabelPrefab;

    public float labelHeightShift = -30f;
    public float labelHeightShiftMultiplier = 0;

    public float completedTaskOpacity = 0.75f;



    // METHODS

    // Create the given task's label.
    public void CreateLabelForTask(TaskManager myTaskManager)
    {
        GameObject currentTaskLabel = Instantiate(taskLabelPrefab, gameObject.transform);
        currentTaskLabel.transform.position = new Vector3(currentTaskLabel.transform.position.x, currentTaskLabel.transform.position.y + labelHeightShift * labelHeightShiftMultiplier, currentTaskLabel.transform.position.z);
        labelHeightShiftMultiplier++;

        Image currentTaskIcon = currentTaskLabel.GetComponentInChildren<Image>();
        currentTaskIcon.sprite = myTaskManager.taskReference.taskIcon;

        TMPro.TMP_Text currentTaskText = currentTaskLabel.GetComponentInChildren<TMPro.TMP_Text>();
        textPerTask.Add(myTaskManager, currentTaskText);
        UpdateTaskLabel(myTaskManager);

        myTaskManager.myTasklistUI = this;
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
