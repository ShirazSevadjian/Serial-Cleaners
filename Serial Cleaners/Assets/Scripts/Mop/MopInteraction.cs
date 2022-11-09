using UnityEngine;

public class MopInteraction : Interactable
{
    [SerializeField] private Transform rightHandPosition;
    [SerializeField] private Transform leftHandPosition;

    [SerializeField] private GameObject canvas;

    private Rigidbody _rigidbody;
    private CapsuleCollider _capsuleCollider;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    protected override void Interact()
    {
        if (player != null)
        {
            player.GetComponent<PlayerInteractable>().Attach(this.gameObject, leftHandPosition, rightHandPosition);
            _capsuleCollider.enabled = false;
            _collider.enabled = false;
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            canvas.SetActive(false);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        canvas.SetActive(true);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        canvas.SetActive(false);
    }

    public override void Detach()
    {
        canvas.SetActive(true);
        _capsuleCollider.enabled = true;
        _collider.enabled = true;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
    }

}
