using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelInfo
{
    public int index;
    public bool completed;
    public bool unlocked;
    public float bestTime;
    public int stars;

    public LevelInfo()
    {
        index = 0;
        completed = false;
        unlocked = false;
        bestTime = 0.0f;
        stars = 0;
    }

    public LevelInfo(int index, bool completed)
    {
        this.index = index;
        this.completed = completed;
        this.unlocked = completed;
        bestTime = 0.0f;
        stars = 0;
    }
}
