using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ReplaceableTarget : MonoBehaviour
{
    // Variables
    [SerializeField] private BoxCollider targetAreaCollider;
    [SerializeField] private Material targetAreaMaterial;

    private float defaultOpacity;
    private Color defaultEmission;
    [SerializeField] private float brightOpacity;
    [SerializeField, ColorUsageAttribute(true, true)] private Color brightEmission;

    [SerializeField] private GameObject myObject;
    [SerializeField] private ReplaceableInteractable myObjectRI;
    [SerializeField] private Transform targetTransform;


    // METHODS
    private void Awake()
    {
        targetAreaCollider = gameObject.GetComponent<BoxCollider>();
        targetAreaMaterial = gameObject.GetComponent<MeshRenderer>().material = Instantiate<Material>(gameObject.GetComponent<MeshRenderer>().material);
        targetTransform = gameObject.GetComponentInChildren<Transform>();

        if (myObjectRI == null)
            myObjectRI = myObject.GetComponent<ReplaceableInteractable>();

        defaultOpacity = targetAreaMaterial.color.a;
        defaultEmission = targetAreaMaterial.GetColor("_EmissionColor");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(other.gameObject);

        // The object was returned to its rightful spot.
        if (other.gameObject == myObject)
        {
            // Alert the object.
            myObjectRI.onRePlace();

            // Snap it into place.
            myObject.transform.position = targetTransform.position;
            myObject.transform.rotation = targetTransform.rotation;

            // Deactivate the target.
            gameObject.SetActive(false);
        }
    }

    // Visual changes.
    public void Shine()
    {
        targetAreaMaterial.color = new Color(targetAreaMaterial.color.r, targetAreaMaterial.color.g, targetAreaMaterial.color.b, brightOpacity);
        targetAreaMaterial.SetColor("_EmissionColor", brightEmission);
    }

    public void Dim()
    {
        targetAreaMaterial.color = new Color(targetAreaMaterial.color.r, targetAreaMaterial.color.g, targetAreaMaterial.color.b, defaultOpacity);
        targetAreaMaterial.SetColor("_EmissionColor", defaultEmission);
    }

    
}
