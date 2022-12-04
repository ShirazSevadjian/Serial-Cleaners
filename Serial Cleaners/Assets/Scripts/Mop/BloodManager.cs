using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BloodManager : TaskManager
{
    [SerializeField] private Texture2D brushTexture; // We need to figure out how to set these either from the code, or from the project files.
    [SerializeField] private Gradient colorGradient;

    [SerializeField] private List<GameObject> bloodPuddles;

    [SerializeField] private UnityEvent onPuddlesDone;
    [SerializeField] private UnityEvent onOnePuddleDonce;


    public static BloodManager Instance { get; private set; }
    public bool AllPuddlesCleaned { get; private set; }
    public Texture2D BrushTexture { get => brushTexture; }
    public Gradient ColorGradient { get => colorGradient; }


    // METHODS
    private void Awake()
    {
        // Handle the singleton pattern.
        if (Instance != null && Instance != this) 
            Destroy(this); 
        else Instance = this;
        // AllPuddlesCleaned = false; This this set by default in the task manager parent class.

        // Collect the puddles into our list.
        BloodPuddle[] boodPuddlesArray = FindObjectsOfType<BloodPuddle>();

        bloodPuddles = new List<GameObject>();
        foreach (BloodPuddle bP in boodPuddlesArray)
            bloodPuddles.Add(bP.gameObject);
    }


    public void RemovePuddle(BloodPuddle puddle)
    {
        if (puddle == null) return;

        IncreaseTaskCompletion();

        bloodPuddles.Remove(puddle.gameObject);
        onOnePuddleDonce.Invoke();
    }

    private void OnTaskDone()
    {
        base.OnTaskDone();

        onPuddlesDone.Invoke();
        AllPuddlesCleaned = true;
    }
}
