using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposalZone : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    [SerializeField] private GameObject particleEffectBody;
    [SerializeField] private GameObject particleEffectEvidence;


    // METHODS
    void Start()
    {
        myCollider = gameObject.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verify whether the colliding object is indeed a body.
        if (other.gameObject.name == "Spine_00") 
        {
            // Debug.Log("A body is being disposed of.");
            // All required actions will be called from the body's "OnDestroy" function.
            Destroy(other.gameObject.GetComponentInParent<BodybagInteraction>().gameObject);

            // Do particle effect.
            GameObject effect = Instantiate(particleEffectBody, other.transform.position, Quaternion.identity);;
            Destroy(effect, 1f);
        }

        // Else, verify that it is a destructible object.
        else if (other.gameObject.GetComponent<GenericInteractable>() != null)
        {
            Debug.Log("An object is being disposed of.");
            Destroy(other.gameObject);

            // Do particle effect.
            GameObject effect = Instantiate(particleEffectEvidence, other.transform.position, Quaternion.identity); ;
            Destroy(effect, 1f);
        }
    }
}
