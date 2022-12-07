using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObjects/Level Parameters", order = 1)]
public class LevelParameters : ScriptableObject
{
    public int index;
    public string lvlName; // The level's name as a string.
    
    public float baseLvlDuration; // The level's base time duration in second.

    // Tasks included.
    public TaskAndQuantity[] tasksInvolved;

    // Special events, and their likelyhood.


    // UTILITIES.
    // Task & Quantity struct for ease of use in inspector.
    [System.Serializable]
    public class TaskAndQuantity
    {
        public Task taskReference;
        public int doTaskXTimes;
    }
}
