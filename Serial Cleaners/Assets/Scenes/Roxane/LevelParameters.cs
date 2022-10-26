using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Level_", menuName = "ScriptableObjects/Level Parameters", order = 1)]
public class LevelParameters : ScriptableObject
{
    public string lvlName; // The level's name as a string.
    
    public float baseLvlDuration; // The level's time duration in second.
    // will be alterable by difficulty setting.

    // bools or list of tasks included?

    // pool of possible special events, & their likelyhood?
}
