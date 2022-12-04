using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodybagInteraction : Interactable
{
    [SerializeField] protected Rigidbody _rigidbodyGrabPoint;

    private PlayerInteractable currentHandler;


    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }


    protected override void Interact()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerInteractable>().Attach(this.gameObject, leftHandPosition, rightHandPosition))
            {
                currentHandler = player.GetComponent<PlayerInteractable>();

                _collider.enabled = false;
                _rigidbody.useGravity = false;
                //_rigidbody.isKinematic = true; // The ragdoll should not be kinematic.
                canvas.SetActive(false);

                // Manhandle the ragdoll.
                //_rigidbodyGrabPoint.isKinematic = true;

            }
        }
    }

    public override void Detach()
    {
        currentHandler = null;

        _collider.enabled = true;
        _rigidbody.useGravity = true;
        //_rigidbody.isKinematic = false; 
        canvas.SetActive(true);

        //_rigidbodyGrabPoint.isKinematic = false;
    }

    private void OnDestroy()
    {
        currentHandler.Detach();
        Detach();
        
        BodyManager.Instance.DisposeOfBody(gameObject);
    }
}
