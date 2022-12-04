using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private Transform interactionPositon;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHandBone;
    [SerializeField] private Transform rightHandBone;
    [SerializeField] private TwoBoneIKConstraint lBoneConstraint;
    [SerializeField] private TwoBoneIKConstraint rBoneConstraint;

    
    public Transform LeftHand { get { return leftHand; } }
    public Transform RightHand { get { return rightHand; } }

    public Transform LeftHandBone { get { return leftHandBone; } }
    public Transform RightHandBone { get { return rightHandBone; } }



    private Interactable interactableObject;
    //private MopInteraction mopInteraction;

    private bool attached;
    private Transform rTarget;
    private Transform lTarget;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Detach();
        }
    }

    public bool Attach(GameObject interactable, Transform leftHand, Transform rightHand)
    {
        if (interactableObject == null)
        {
            interactable.transform.SetParent(interactionPositon);
            interactable.transform.localPosition = Vector3.zero;
            interactable.transform.rotation = Quaternion.identity;

            if (interactable.TryGetComponent<Interactable>(out interactableObject)) { }


            if (leftHand != null)
            {
                lBoneConstraint.weight = 1.0f;
                lTarget = leftHand;
            }
            if (rightHand != null)
            {
                rBoneConstraint.weight = 1.0f;
                rTarget = rightHand;
            }

            attached = true;

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AttachHandsOnly(GameObject interactable, Transform leftHand, Transform rightHand)
    {
        if (interactableObject == null)
        {
            if (interactable.TryGetComponent<Interactable>(out interactableObject)) { }


            if (leftHand != null)
            {
                lBoneConstraint.weight = 1.0f;
                lTarget = leftHand;
            }
            if (rightHand != null)
            {
                rBoneConstraint.weight = 1.0f;
                rTarget = rightHand;
            }

            attached = true;

            return true;
        }
        else
        {
            return false;
        }
    }

    public void Detach()
    {
        if (interactableObject != null)
        {
            lBoneConstraint.weight = 0.0f;
            rBoneConstraint.weight = 0.0f;
            attached = false;

            interactableObject.Detach();
            interactableObject.transform.SetParent(null);
            interactableObject = null;
        }
    }

    public void FixedUpdate()
    {
        if (attached)
        {
            if (lTarget != null)
            {
                leftHand.position = lTarget.position;
                leftHand.localRotation = lTarget.localRotation;
            }
            if (rTarget != null)
            {
                rightHand.position = rTarget.position;
                rightHand.localRotation = rTarget.localRotation;
            }
        }
    }
}
