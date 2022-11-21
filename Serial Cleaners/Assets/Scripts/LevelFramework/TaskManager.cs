using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Task taskReference;
    
    // Housekeeping.
    private bool isTaskDone = false;
    public bool IsTaskDone { get { return CheckIfTaskDone(); } set { isTaskDone = value; } } // Allow other scripts to forcibly mark a task as being done?

    [SerializeField] private int doXTimesInTotal;
    [SerializeField] private int doneXTimesSoFar;

    // For use by the UI.
    public int DoXTimesInTotal { get; }
    public int DoneXTimesSoFar { get; }


    // Associated UI component.
    [SerializeField] private TasklistUI myTasklistUI;


    // METHODS
    // Set the managers' parameters from the input data.
    public void PrepareManager(Task taskReference, int doXTimesInTotal, TasklistUI tasklistUI)
    {
        this.taskReference = taskReference;
        this.doXTimesInTotal = doXTimesInTotal;
        doneXTimesSoFar = 0;

        this.myTasklistUI = tasklistUI;
        // Set target text in tasklistUI;
    }

    // Increase the task' completion counter.
    // Could theoretically also be used to decrease it.
    public void IncreaseTaskCompletion(int doneXTimes = 1)
    {
        doneXTimesSoFar += doneXTimes;
        // Update task UI text.

        // Check if the task is now complete.
        CheckIfTaskDone();
    }

    // Return whether the task has been completed.
    public bool CheckIfTaskDone()
    {
        return isTaskDone = (doneXTimesSoFar == doXTimesInTotal);
    }
}
