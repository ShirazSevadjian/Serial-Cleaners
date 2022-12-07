using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Singleton pattern.
    public static LevelManager Instance { get; private set; }

    // Level info scriptable object.
    [SerializeField] private LevelParameters currentLvl;

    // Pause effect.
    [SerializeField] private GameObject pausePannel;
    [SerializeField] private GameObject mainPausePanel;
    [SerializeField] private GameObject optionsPausePanel;
    [SerializeField] private TMPro.TMP_Text timeRemaining_Txt;

    // Parameters.
    [SerializeField] private float remainingTimerDuration;
    public bool gamePaused;

    // Task managers.
    [SerializeField] private TaskManager[] taskManagers;
    // Tasklist UI.
    [SerializeField] private TasklistUI taskListUI;


    // Is the level currently running?
    bool isLevelActive = false;

    public float difficultyMultiplier = 1; // Temporary, integrate difficulty as its own thing.

    // EVENTS.
    public UnityEvent_float levelStartEvent;
    public UnityEvent_string levelEndEvent;
    public UnityEvent victoryEvent;
    public UnityEvent defeatEvent;
    public UnityEvent_float levelTimerTick;

    // private bool allBodies = false;
    // private bool allPuddlesCleaned = false;

    void LevelEventListener() { Debug.Log("There was an event."); }
    void LevelEventListener(string message) { Debug.Log("There was an event. " + message); }
    void LevelEventListener(float value) { } //Debug.Log("There was an event, value of " + value + ".") ; 


    // CONSTRUCTOR.
    public LevelManager(LevelParameters initialLevelParameters) // May be redundant with the "Awake" function.
    {
        // Set initial parameters.
        currentLvl = initialLevelParameters;
        SetParameters(currentLvl);
    }


    // METHODS
    private void Awake()
    {
        // Handling of the Singleton pattern.
        if (Instance != null && Instance != this)
        {
            Debug.Log(this + " will be destroyed as it is not the singleton instance, " + Instance);
            Destroy(this);
        }
        else
            Instance = this;

        // Set parameters.
        SetParameters(currentLvl);

        // Register events.
        levelStartEvent.AddListener(LevelEventListener);
        levelEndEvent.AddListener(LevelEventListener);
        victoryEvent.AddListener(LevelEventListener);
        defeatEvent.AddListener(LevelEventListener);
        levelTimerTick.AddListener(LevelEventListener);

        // Start the level. Delay?
        levelTimerTick.Invoke(remainingTimerDuration);
        StartLevel();
    }

    private void Update()
    {
        // Handle pausing.
        if (Input.GetButtonDown("Pause"))
        {
            if (gamePaused)
                ResumeGame();
            else PauseGame();
        }

    }


    // MANAGEMENT METHODS.
    private void SetParameters(LevelParameters lvlParams)
    {
        SetTimerDuration(lvlParams.baseLvlDuration);
    }

    public void ResetParameters(LevelParameters lvlParams, bool updateCurrentLvlSO = true) // Reset the level's parameters to those of the inputed SO.
    {
        // If requested, change the currentLvl SO reference.
        if (updateCurrentLvlSO)
            currentLvl = lvlParams;

        // Set parameters.
        SetParameters(lvlParams);
    }

    public void ResetParameters() // Reset the level's parameters to those found in the currentLvl SO reference.
    {
        ResetParameters(currentLvl);
    }

    // These are public so that other scripts can call them using the level manager's instance.
    // Start of level method.
    public void StartLevel(bool resetTimer = false)
    {
        if (!isLevelActive)
        {
            Debug.Log("Level is starting.");

            // Set up task managers from those referenced in the level's parameters.
            SetUpTasksAndManagers();

            // If requested, restart the timer.
            if (resetTimer)
                SetTimerDuration(currentLvl.baseLvlDuration);

            // Start timer coroutine.
            timerDecreaseCoroutine = StartCoroutine(DecreaseTimer());

            isLevelActive = true;
            levelStartEvent.Invoke(remainingTimerDuration);
        }
    }

    private void SetUpTasksAndManagers()
    {
        taskManagers = new TaskManager[currentLvl.tasksInvolved.Length];
        LevelParameters.TaskAndQuantity taskInfo;

        for (int i = 0; i < currentLvl.tasksInvolved.Length; i++)
        {
            taskInfo = currentLvl.tasksInvolved[i];
            System.Type type = System.Type.GetType(taskInfo.taskReference.taskManagerClassName);

            if (type != null)
            {
                // Create the associated task manager. 
                // Have it already exist as a prefab instead?
                taskManagers[i] = UnityEditor.ObjectFactory.AddComponent(gameObject, type) as TaskManager;
                taskManagers[i].PrepareManager(taskInfo.taskReference, taskInfo.doTaskXTimes, taskListUI);

                // Create associated UI element in the taskListUI.
                taskListUI.CreateLabelForTask(taskManagers[i]);
            }
        }
    }

    // End of level method.
    public void EndLevel()
    {
        if (isLevelActive)
        {
            isLevelActive = false;
            Time.timeScale = 0.0f;
            // Emit end of level event in case other scripts need it.
            levelEndEvent.Invoke("The level has ended.");

            //End timer coroutine.
            if (timerDecreaseCoroutine != null)
                StopCoroutine(timerDecreaseCoroutine);
            Debug.Log(string.Format("There are {0} seconds left to the timer.", remainingTimerDuration));
            // Save the remaining time duration to a text file.
            timeRemaining_Txt.text = remainingTimerDuration.ToString() + "s";

            // Do checks. Was everything properly cleaned up?
            if (VictoryConditionsMet())
                DoVictory();
            else DoFailure();
        }
    }

    /*
    public void LevelChecker() // What is this used for?
    {
        if (BloodManager.Instance.AllPuddlesCleaned && BodyManager.Instance.AllBodiesCleaned)
        {
            EndLevel();
        }
    }
    */

    private bool VictoryConditionsMet()
    {
        // Loop through the task managers and verify whether their tasks have been completed.
        foreach (TaskManager tm in taskManagers)
        {
            // Return false as soon as one has not.
            if (!tm.IsTaskDone)
                return false;
        }
        // Else, return true.
        return true;
    }

    private void DoVictory()
    {
        victoryEvent.Invoke();
        GlobalLevelManager.CompletedLevel(currentLvl.index, remainingTimerDuration, 2);
    }

    private void DoFailure()
    {
        defeatEvent.Invoke();
    }

    // TIMER.
    private void SetTimerDuration(float baseLevelDuration)
    {
        remainingTimerDuration = baseLevelDuration * difficultyMultiplier;
        levelTimerTick.Invoke(remainingTimerDuration);
    }

    Coroutine timerDecreaseCoroutine;

    // Timer decrease coroutine.
    IEnumerator DecreaseTimer()
    {
        yield return new WaitForSeconds(1f);

        // Decrease the remaining time by 1 second.
        while (remainingTimerDuration > 0)
        {
            remainingTimerDuration--;
            levelTimerTick.Invoke(remainingTimerDuration);

            yield return new WaitForSeconds(1f);
        }

        // Send end of level event.
        EndLevel();
    }

    // PAUSE
    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePannel.SetActive(true);

        gamePaused = true;
        Debug.Log("The game was paused.");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePannel.SetActive(false);

        gamePaused = false;
        Debug.Log("The game has resumed.");
    }
}
