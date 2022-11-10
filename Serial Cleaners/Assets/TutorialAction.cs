using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialAction : MonoBehaviour
{

    [SerializeField] private UnityEvent interacted;

    private bool _isInside;

    protected virtual void Update()
    {
        // If players inputs either "e" or "X" button in the controller
        if (_isInside)
        {
            if (Input.GetButtonDown("Interact"))
            {
                interacted.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInside = false;
        }
    }
}
