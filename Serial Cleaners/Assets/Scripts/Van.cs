using UnityEngine;

public class Van : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInteractable interactableComponent = other.GetComponent<PlayerInteractable>();
            if (interactableComponent.InteractableObject != null && interactableComponent.InteractableObject.CompareTag("Destroyable"))
            {    
                Interactable reference = interactableComponent.Detach();
                Destroy(reference.gameObject, 1.0f);
            }
        }
    }
}
