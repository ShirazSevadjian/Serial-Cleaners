using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    // Event system.
    // start event
    // victory & defeat events

    // ui?



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

        // Set parameters.
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

        // Do not destroy on load.
        DontDestroyOnLoad(this);

        // Set parameters.
        SetParameters(currentLvl);

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

    




    public void StartLevel(bool resetTimer = false)
    {
        isLevelActive = true;

        // If requested, restart the timer.
        if (resetTimer)
            SetTimerDuration(currentLvl.baseLvlDuration);

        // Start timer coroutine.
        StartCoroutine(DecreaseTimer());
    }

    // different restart level?



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

            // update UI, send event?

            yield return new WaitForSeconds(1f);
        }

        // do end of game events
    }

    




    



}
