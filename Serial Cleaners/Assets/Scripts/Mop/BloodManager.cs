using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BloodManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onPuddlesDone;
    [SerializeField] private UnityEvent onOnePuddleDonce;
    [SerializeField] private List<GameObject> bloodPuddles;

    public static BloodManager Instance { get; private set; }

    private void Awake() { if (Instance != null && Instance != this) Destroy(gameObject); else Instance = this; }

    public void RemovePuddle(BloodPuddle puddle)
    {
        if (puddle == null) return; 

        bloodPuddles.Remove(puddle.gameObject);
        onOnePuddleDonce.Invoke();

        if (bloodPuddles.Count == 0)
        {
            onPuddlesDone.Invoke();
        }
    }
}
