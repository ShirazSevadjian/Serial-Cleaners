using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateJoint : MonoBehaviour
{

    public GameObject player;
    public float pickupRange = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            RaycastHit hit;

            if (Physics.Raycast(player.transform.position, player.transform.TransformDirection(Vector3.forward), out hit, pickupRange))
            {
                Debug.Log("Hit");
                SpringJoint joint = player.AddComponent<SpringJoint>();
                joint.anchor = hit.point;
                joint.connectedBody = hit.transform.GetComponent<Rigidbody>();
                joint.enableCollision = false;

                //Physics.IgnoreCollision(player.GetComponent<Collider>(), hit.transform.GetComponent<Collider>(), true);

            }
        }
    }
}
