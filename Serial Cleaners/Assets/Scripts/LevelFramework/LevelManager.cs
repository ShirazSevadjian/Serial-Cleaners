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

    // Parameters.
    [SerializeField] private float remainingTimerDuration;
    public bool gamePaused;
    // Other stuff

    // Is the level currently running?
    bool isLevelActive = false;


    public float difficultyMultiplier = 1; // Temporary, integrate difficulty as its own thing.




    // EVENTS.
    public UnityEvent_float levelStartEvent;
    public UnityEvent_string levelEndEvent;
    public UnityEvent victoryEvent;
    public UnityEvent defeatEvent;
    public UnityEvent_float levelTimerTick;

    void LevelEventListener() { Debug.Log("There was an event."); }
    void LevelEventListener(string message) { Debug.Log("There was an event. " + message); }
    void LevelEventListener(float value) { } //Debug.Log("There was an event, value of " + value + ".") ; 


    // CONSTRUCTOR.
    public LevelManager(LevelParameters initialLevelParameters) // May be redundant with the "Awake" function.
    {
        // Handling of the Singleton pattern.
        if (Instance != null && Instance != this)
        {
            Debug.Log(this + " will be destroyed as it is not the singleton instance, " + Instance);
            Destroy(this);
        }
        else
        {
            Debug.Log("Created the singleton " + this);
            Instance = this;
        }

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
        //StartLevel();
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

            // If requested, restart the timer.
            if (resetTimer)
                SetTimerDuration(currentLvl.baseLvlDuration);

            // Start timer coroutine.
            timerDecreaseCoroutine = StartCoroutine(DecreaseTimer());

            isLevelActive = true;
            levelStartEvent.Invoke(remainingTimerDuration);
        }
    }

    // End of level method.
    public void EndLevel()
    {
        if (isLevelActive)
        {
            isLevelActive = false;

            // Emit end of level event in case other scripts need it.
            levelEndEvent.Invoke("The level has ended.");

            //End timer coroutine.
            if (timerDecreaseCoroutine != null)
                StopCoroutine(timerDecreaseCoroutine);
            Debug.Log(string.Format("There are {0} seconds left to the timer.", remainingTimerDuration));

            // Do checks. Was everything properly cleaned up?
        }
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

    private void ResumeGame()
    {
        Time.timeScale = 1;
        pausePannel.SetActive(false);

        gamePaused = false;
        Debug.Log("The game has resumed.");
    }
}
