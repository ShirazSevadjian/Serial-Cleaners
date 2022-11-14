using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected GameObject canvas;
    [SerializeField] protected Transform rightHandPosition;
    [SerializeField] protected Transform leftHandPosition;

    protected SphereCollider _collider;
    protected Rigidbody _rigidbody;

    private bool _isInside;

    protected GameObject player;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
            canvas.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInside = false;
            player = null;
            canvas.SetActive(false);
        }
    }

    protected virtual void Interact()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerInteractable>().Attach(this.gameObject, leftHandPosition, rightHandPosition))
            {
                transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("InteractableObject");
                _collider.enabled = false;
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = true;
                canvas.SetActive(false);
            }
        }
    }

    public virtual void Detach()
    {
        transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer("Default");
        _collider.enabled = true;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        canvas.SetActive(true);
    }
}
