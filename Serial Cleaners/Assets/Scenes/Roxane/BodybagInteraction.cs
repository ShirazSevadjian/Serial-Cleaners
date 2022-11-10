using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodybagInteraction : Interactable
{
    [SerializeField] protected Rigidbody _rigidbodyShoulders;
    [SerializeField] protected GameObject _shoulderTransform;

    private FixedJoint jointLeft;
    private FixedJoint jointRight;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponentInChildren<Rigidbody>();

        _shoulderTransform = _rigidbodyShoulders.gameObject;
    }


    protected override void Interact()
    {
        if (player != null)
        {
            if (player.GetComponent<PlayerInteractable>().Attach(this.gameObject, leftHandPosition, rightHandPosition))
            {
                _collider.enabled = false;
                _rigidbody.useGravity = false;
                _rigidbody.isKinematic = false;
                canvas.SetActive(false);


                // Manhandle the ragdoll.
                /*
                jointLeft = _shoulderTransform.AddComponent<FixedJoint>();
                jointRight = _shoulderTransform.AddComponent<FixedJoint>();

                jointLeft.connectedBody = player.GetComponent<Rigidbody>();
                jointRight.connectedBody = player.GetComponent<Rigidbody>();

                jointLeft.anchor = player.GetComponent<PlayerInteractable>().LeftHand.position;
                jointRight.anchor = player.GetComponent<PlayerInteractable>().RightHand.position;

                jointLeft.enableCollision = false;
                jointRight.enableCollision = false;
                */
            }
        }
    }

    public override void Detach()
    {
        _collider.enabled = true;
        _rigidbody.useGravity = true;
        //_rigidbody.isKinematic = false;
        canvas.SetActive(true);

        /*
        // Unhandle the ragdoll.
        Destroy(jointLeft);
        Destroy(jointRight);
        */
    }

}
