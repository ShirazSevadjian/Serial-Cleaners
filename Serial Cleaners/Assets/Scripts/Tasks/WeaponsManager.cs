using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : TaskManager
{
    [SerializeField] private List<GameObject> weapons;

    public static WeaponsManager Instance { get; private set; }


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
        // Collect the weapons / evidence into our list.
        GenericInteraction[] weaponsArray = FindObjectsOfType<GenericInteraction>();

        weapons = new List<GameObject>();
        foreach (GenericInteraction body in weaponsArray)
        {
            weapons.Add(body.gameObject);
        }
    }

    public void DisposeOfWeapon(GameObject weapon)
    {
        if (weapon == null) return;

        weapons.Remove(weapon.gameObject);

        IncreaseTaskCompletion();

        // On single object destroyed event.
    }

    new private void OnTaskDone()
    {
        base.OnTaskDone();

        // On Task Completion Event
    }
}
