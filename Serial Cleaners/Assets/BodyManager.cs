using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BodyManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onBodiesDone;
    [SerializeField] private UnityEvent onOneBodyDestroyed;
    [SerializeField] private List<GameObject> bodies;

    public static BodyManager Instance { get; private set; }
    public bool AllBodiesCleaned { get; private set; }

    private void Awake() { if (Instance != null && Instance != this) Destroy(gameObject); else Instance = this; AllBodiesCleaned = false; }

    public void RemoveBody(GameObject body)
    {
        if (body == null) return;

        bodies.Remove(body.gameObject);
        onOneBodyDestroyed.Invoke();

        if (bodies.Count == 0)
        {
            onBodiesDone.Invoke();
            AllBodiesCleaned = true;
        }
    }
}
