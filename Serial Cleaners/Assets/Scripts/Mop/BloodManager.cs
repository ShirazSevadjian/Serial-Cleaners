using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BloodManager : MonoBehaviour
{
    [SerializeField] private Texture2D brushTexture;
    [SerializeField] private UnityEvent onPuddlesDone;
    [SerializeField] private UnityEvent onOnePuddleDonce;
    [SerializeField] private List<GameObject> bloodPuddles;
    [SerializeField] private Gradient colorGradient;


    public static BloodManager Instance { get; private set; }
    public bool AllPuddlesCleaned { get; private set; }
    public Texture2D BrushTexture { get => brushTexture; }
    public Gradient ColorGradient { get => colorGradient; }

    private void Awake() { if (Instance != null && Instance != this) Destroy(gameObject); else Instance = this;  AllPuddlesCleaned = false; }

    public void RemovePuddle(BloodPuddle puddle)
    {
        if (puddle == null) return; 

        bloodPuddles.Remove(puddle.gameObject);
        onOnePuddleDonce.Invoke();

        if (bloodPuddles.Count == 0)
        {
            onPuddlesDone.Invoke();
            AllPuddlesCleaned = true;
        }
    }
}
