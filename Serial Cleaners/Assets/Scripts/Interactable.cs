using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Interactable : MonoBehaviour
{
    protected SphereCollider _collider;

    private bool _isInside;

    protected GameObject player;

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
        if (other.CompareTag("Player"))
        {
            _isInside = true;
            player = other.gameObject;
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInside = false;
            player = null;
        }
    }

    protected abstract void Interact();
    public abstract void Detach();
}
