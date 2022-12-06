using System.Collections;
using UnityEngine;

public class MopCleaner : MonoBehaviour
{
    [SerializeField] private float dryTimer = 10.0f;

    private WaitForSeconds waitForDry;
    private Coroutine dryRoutine;

    private bool wet;

    public bool Wet { get => wet; }

    private void Start()
    {
        waitForDry = new WaitForSeconds(dryTimer);
    }

    public void SetWet()
    {
        if (dryRoutine != null) StopCoroutine(dryRoutine);
        wet = true;
        dryRoutine = StartCoroutine(DryTimer());
    }

    private IEnumerator DryTimer()
    {
        yield return waitForDry;
        wet = false;
    }
}
