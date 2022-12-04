using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BodyManager : TaskManager
{
    [SerializeField] private List<GameObject> bodies;

    [SerializeField] private UnityEvent onBodiesDone; // These events will have to be handled differently.
    [SerializeField] private UnityEvent onOneBodyDestroyed;
    

    public static BodyManager Instance { get; private set; }
    public bool AllBodiesCleaned { get { return CheckIfTaskDone(); } }


    // METHODS
    private void Awake()
    {
        // Handle the singleton pattern.
        if (Instance != null && Instance != this)
            Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        // Collect the bodies into our list.
        BodybagInteraction[] bodybagsArray = FindObjectsOfType<BodybagInteraction>();

        bodies = new List<GameObject>();
        foreach (BodybagInteraction body in bodybagsArray)
        {
            bodies.Add(body.gameObject);
        }

        // Connect the body disposal script to this.
    }

    public void DisposeOfBody(GameObject body)
    {
        if (body == null) return;

        bodies.Remove(body.gameObject);

        IncreaseTaskCompletion();

        onOneBodyDestroyed.Invoke();
    }

    new private void OnTaskDone()
    {
        base.OnTaskDone();

        onBodiesDone.Invoke();
    }
}
