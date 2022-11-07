using UnityEngine;

public class MopInteraction : Interactable
{
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
        _capsuleCollider.enabled = false;
        _collider.enabled = false;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
    }

    public override void Detach()
    {
        
    }

}
