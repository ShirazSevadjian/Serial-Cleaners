using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Interactable : MonoBehaviour
{
    protected SphereCollider _collider;

    private bool _isInside;

    protected virtual void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.isTrigger = true;
    }

    protected virtual void Update()
    {
        // If players inputs either "e" or "X" button in the controller
        if (_isInside)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Interact();
            } 
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _isInside = true;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) _isInside = false;
       
    }

    protected abstract void Interact();
    public abstract void Detach();
}
