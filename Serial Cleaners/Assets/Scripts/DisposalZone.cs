using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposalZone : MonoBehaviour
{
    [SerializeField] private Collider myCollider;


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
        }
    }
}
