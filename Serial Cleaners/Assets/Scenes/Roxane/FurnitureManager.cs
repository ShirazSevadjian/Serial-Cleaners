using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureManager : TaskManager
{
    [SerializeField] private List<GameObject> furniture;

    public static FurnitureManager Instance { get; private set; }


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
        // Collect the furniture items into our list.
        ReplaceableInteractable[] furnitureArray = FindObjectsOfType<ReplaceableInteractable>();

        furniture = new List<GameObject>();
        foreach (ReplaceableInteractable item in furnitureArray)
        {
            furniture.Add(item.gameObject);
        }
    }

    public void FurnitureRePlaced(GameObject item)
    {
        if (item == null) return;

        furniture.Remove(item.gameObject);

        IncreaseTaskCompletion();

        // On single object destroyed event.
    }

    new private void OnTaskDone()
    {
        base.OnTaskDone();

        // On Task Completion Event
    }
}
