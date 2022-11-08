using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    // Singleton pattern.
    public static LevelManager Instance { get; private set; }
    
    // Level info scriptable object.
    [SerializeField] private LevelParameters currentLvl;

    // Parameters.
    [SerializeField] private float remainingTimerDuration;
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
    void LevelEventListener(float value) { Debug.Log("There was an event, value of " + value + ".") ; }


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
        StartLevel();
    }


    // MANAGEMENT METHODS.
    private void SetParameters(LevelParameters lvlParams)
    {
        SetTimerDuration(lvlParams.baseLvlDuration);
    }

    public void ResetParametersFromSO(LevelParameters lvlParams, bool updateCurrentLvlSO = true) // Reset the level's parameters to those of the inputed SO.
    {
        // If requested, change the currentLvl SO reference.
        if (updateCurrentLvlSO)
            currentLvl = lvlParams;

        // Set parameters.
        SetParameters(lvlParams);
    }

    public void ResetParametersFromSO() // Reset the level's parameters to those found in the currentLvl SO reference.
    {
        ResetParametersFromSO(currentLvl);
    }

    



    // 
    public void StartLevel(bool resetTimer = false)
    {
        // If requested, restart the timer.
        if (resetTimer)
            SetTimerDuration(currentLvl.baseLvlDuration);


        // Update UI elements.
        levelTimerTick.Invoke(remainingTimerDuration);

        // Start timer coroutine.
        StartCoroutine(DecreaseTimer());

        isLevelActive = true;
        levelStartEvent.Invoke(remainingTimerDuration);
    }




    // TIMER.
    private void SetTimerDuration(float baseLevelDuration)
    {
        remainingTimerDuration = baseLevelDuration * difficultyMultiplier;
    }

    // Timer decrease coroutine.
    IEnumerator DecreaseTimer()
    {
        // Decrease the remaining time by 1 second.
        while (remainingTimerDuration > 0)
        {
            remainingTimerDuration--;
            levelTimerTick.Invoke(remainingTimerDuration);

            yield return new WaitForSeconds(1f);
        }

        // Send end of level event.
        levelEndEvent.Invoke("The level has ended.");
    }
}
