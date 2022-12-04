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
    public int DoXTimesInTotal { get { return doXTimesInTotal; } }
    public int DoneXTimesSoFar { get { return doneXTimesSoFar; } }


    // Associated UI component.
    public TasklistUI myTasklistUI;


    // METHODS
    // Set the managers' parameters from the input data.
    public void PrepareManager(Task taskReference, int doXTimesInTotal, TasklistUI tasklistUI)
    {
        this.taskReference = taskReference;
        this.doXTimesInTotal = doXTimesInTotal;
        doneXTimesSoFar = 0;

        this.myTasklistUI = tasklistUI;
    }

    // Increase the task' completion counter.
    // Could theoretically also be used to decrease it.
    public void IncreaseTaskCompletion()
    {
        doneXTimesSoFar++;

        myTasklistUI.UpdateTaskLabel(this);

        // Check if the task is now complete.
        if (CheckIfTaskDone())
            OnTaskDone();
    }

    // Return whether the task has been completed.
    public bool CheckIfTaskDone()
    {
        return isTaskDone = (doneXTimesSoFar == doXTimesInTotal);
    }

    // Do when the task is done.
    protected void OnTaskDone()
    {
        Debug.Log(string.Format("The task '{0}' has been completed.", taskReference.taskText));
    }
}
