using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceableInteractable : Interactable
{
    // VARIABLES    
    private PlayerInteractable currentHandler;

    [SerializeField] private MeshCollider _nonTriggerCollider;

    [SerializeField] private ReplaceableTarget myTarget;


    // METHODS
    protected override void Awake()
    {
        base.Awake();

        _nonTriggerCollider = gameObject.GetComponent<MeshCollider>();
    }

    protected override void Interact()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerInteractable>().Attach(this.gameObject, leftHandPosition, rightHandPosition))
            {
                currentHandler = player.GetComponent<PlayerInteractable>();

                Physics.IgnoreCollision(_nonTriggerCollider, player.GetComponent<Collider>(), true);

                _collider.enabled = false;
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = true;
                canvas.SetActive(false);

                // Update the target's visuals.
                myTarget.Shine();
            }
        }
    }

    public override void Detach()
    {
        currentHandler = null;

        Physics.IgnoreCollision(_nonTriggerCollider, player.GetComponent<Collider>(), false);

        _collider.enabled = true;
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        canvas.SetActive(true);

        // Update the target's visuals.
        myTarget.Dim();
    }

    public void onRePlace()
    {
        currentHandler.Detach();
        Detach();

        // Destroy the various interactable components.
        Destroy(this);
        Destroy(canvas);
        Destroy(_rigidbody);
        Destroy(_collider);

        // Update the manager.
    }


}
