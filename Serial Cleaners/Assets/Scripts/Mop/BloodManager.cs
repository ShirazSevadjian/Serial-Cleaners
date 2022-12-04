using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BloodManager : TaskManager
{
    [SerializeField] private Texture2D brushTexture; // We need to figure out how to set these either from the code, or from the project files.
    [SerializeField] private Gradient colorGradient = new Gradient();

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
        {
            bP.ConnectBloodManager();
            bloodPuddles.Add(bP.gameObject);
        }

        // Populate the gradient with the proper colours. 
        // Not optimal atm.
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        colorKeys[0].color = new Color(0.55f, 0.017f, 0.023f, 1f);
        colorKeys[0].time = 0.0f;
        colorKeys[1].color = new Color(0.3f, 0.15f, 0.1f, 1f);
        colorKeys[1].time = 1.0f;
        colorGradient.colorKeys = colorKeys;
    }


    public void RemovePuddle(BloodPuddle puddle)
    {
        if (puddle == null) return;

        IncreaseTaskCompletion();

        bloodPuddles.Remove(puddle.gameObject);
        onOnePuddleDonce.Invoke(); // These events will have to be handled differently.
    }

    new private void OnTaskDone()
    {
        base.OnTaskDone();

        onPuddlesDone.Invoke();
        AllPuddlesCleaned = true;
    }
}
