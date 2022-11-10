using UnityEngine;

public class MopInteraction : Interactable
{
    private CapsuleCollider _capsuleCollider;

    protected override void Awake()
    {
        base.Awake();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    protected override void Interact()
    {
        base.Interact();
        if (player != null)
        {
           // _capsuleCollider.enabled = false;
        }
    }

    public override void Detach()
    {
        base.Detach();
        if (player != null) _capsuleCollider.enabled = true;
    }
}
