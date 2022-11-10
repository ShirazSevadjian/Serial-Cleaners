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
                _collider.enabled = false;
                _rigidbody.useGravity = true;
                //_rigidbody.isKinematic = true; // The ragdoll should not be kinematic.
                canvas.SetActive(false);

                // Manhandle the ragdoll.
                
            }
        }
    }

    public override void Detach()
    {
        _collider.enabled = true;
        _rigidbody.useGravity = true;
        //_rigidbody.isKinematic = false; 
        canvas.SetActive(true);



    }

}
