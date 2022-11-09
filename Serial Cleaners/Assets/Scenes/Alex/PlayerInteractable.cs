using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [SerializeField] private Transform interactionPositon;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    private Interactable interactableObject;
    private MopInteraction mopInteraction;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (mopInteraction != null)
            {
                mopInteraction.Detach();
                mopInteraction.transform.SetParent(null);

            }
        }
    }

    public void Attach(GameObject interactable, Transform leftHand, Transform rightHand)
    {
        interactable.transform.SetParent(interactionPositon);
        interactable.transform.localPosition = Vector3.zero;
        interactable.transform.rotation = Quaternion.identity;

        if (interactable.TryGetComponent<MopInteraction>(out mopInteraction))
       
        this.leftHand.position = leftHand.position;
        this.rightHand.position = rightHand.position;
    }
}
