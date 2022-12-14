using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodybagInteraction : Interactable
{
    [SerializeField] protected Rigidbody _rigidbodyGrabPoint;

    private PlayerInteractable currentHandler;

    private FixedJoint myFixedJoint;


    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();

        /*
        if (currentHandler != null)
        {
            Vector3 bonesPositionDelta = (currentHandler.LeftHandBone.position + currentHandler.RightHandBone.position) / 2;
            Debug.Log("Bone position delta: " + bonesPositionDelta);

            //_rigidbodyGrabPoint.position = bonesPositionDelta;
            //_rigidbodyGrabPoint.MovePosition(bonesPositionDelta);

            Debug.Log("Fixed joint's position: " + myFixedJoint.transform.position);
            Debug.Log("Fixed joint's position: " + myFixedJoint.connectedBody.transform.position);
        }
        */
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
                _rigidbodyGrabPoint.useGravity = false;

                //_rigidbody.isKinematic = true; 
                //_rigidbodyGrabPoint.isKinematic = true; // The ragdoll should not be kinematic.

                canvas.SetActive(false);


                // Manhandle the ragdoll.

                Vector3 bonesPositionDelta = (currentHandler.LeftHandBone.position + currentHandler.RightHandBone.position) / 2;
                Debug.Log("Bone position delta: " + bonesPositionDelta);

                _rigidbodyGrabPoint.position = bonesPositionDelta;
                this.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);

                myFixedJoint = player.AddComponent<FixedJoint>();
                myFixedJoint.anchor = _rigidbodyGrabPoint.position;
                myFixedJoint.connectedBody = _rigidbodyGrabPoint;
                myFixedJoint.enableCollision = false;
            }
        }
    }

    public override void Detach()
    {
        currentHandler = null;

        _collider.enabled = true;
        _rigidbody.useGravity = true;
        _rigidbodyGrabPoint.useGravity = true;

        //_rigidbody.isKinematic = false; 
        //_rigidbodyGrabPoint.isKinematic = false;

        canvas.SetActive(true);

        Destroy(myFixedJoint);
    }

    private void OnDestroy()
    {
        if(currentHandler != null)
            currentHandler.Detach();

        // Detach();

        BodyManager.Instance.DisposeOfBody(gameObject);
    }
}
