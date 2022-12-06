using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPersonCarry : MonoBehaviour
{
    public float pickupRange = 5;
    private GameObject heldObject;
    public float moveForce = 100;
    public GameObject player1;
    public GameObject player2;
    public Transform player1HoldPosition;
    public Transform player2HoldPosition;
    private bool player1Holding = false;
    private bool player2Holding = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            RaycastHit hit;

            if (heldObject == null)
            {
                if (Physics.Raycast(player1.transform.position, player1.transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    player1Holding = true;
                    Debug.Log("Found object in range");
                    PickupObject(hit.transform.gameObject, player1HoldPosition);

                    //player1.gameObject.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, 0, 0); 
                    FixedJoint joint = player1.AddComponent<FixedJoint>();
                    joint.anchor = hit.point;
                    joint.connectedBody = hit.transform.GetComponent<Rigidbody>();
                    joint.enableCollision = false;

                    Physics.IgnoreCollision(player1.GetComponent<Collider>(), hit.transform.gameObject.GetComponent<Collider>(), true);
                }
               
            }
            else if(heldObject.transform.parent == player2HoldPosition)
            {
               
                if (Physics.Raycast(player1.transform.position, player1.transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    player1Holding = true;
                    Debug.Log("Entering Condition");
                    //Debug.Log("Found object in range");
                    DropObject();
                    PickupObject(hit.transform.gameObject);
                }
            }
            else if(heldObject.transform.parent == player1HoldPosition)
            {
                DropObject();
                player1Holding = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            RaycastHit hit;

            if(heldObject == null)
            {
                if (Physics.Raycast(player2.transform.position, player2.transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    player2Holding = true;
                    Debug.Log("Found object in range");
                    PickupObject(hit.transform.gameObject, player2HoldPosition);
                }
            }
            else if (heldObject.transform.parent == player1HoldPosition)
            {
                if (Physics.Raycast(player2.transform.position, player2.transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    player2Holding = true;
                    Debug.Log("Found object in range");
                    DropObject();
                    PickupObject(hit.transform.gameObject);
                    player2.gameObject.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, 0, 0);

                    FixedJoint joint = player2.AddComponent<FixedJoint>();
                    joint.anchor = hit.point;
                    joint.connectedBody = hit.transform.GetComponent<Rigidbody>();
                    joint.enableCollision = false;

                    Physics.IgnoreCollision(player2.GetComponent<Collider>(), hit.transform.gameObject.GetComponent<Collider>(), true);
                }
            }
            else if (heldObject.transform.parent == player2HoldPosition)
            {
                DropObject();
                player2Holding = false;
            }
        }


        if (heldObject != null)
        {
            /*MoveObject(player1HoldPosition);
            MoveObject(player2HoldPosition);
            isHoldingObject(player1, heldObject);*/
            if((isHoldingObject(player1, heldObject)) && (isHoldingObject(player2, heldObject)))
            {
                Debug.Log("Inside condition where both players are holding");
                //heldObject.gameObject.GetComponent<Rigidbody>().centerOfMass = new Vector3(0, 0, 0);
                MoveObject(player1HoldPosition);
                
            }
            else if(isHoldingObject(player1, heldObject))
            {
                MoveObject(player1HoldPosition);
            }
            else if(isHoldingObject(player2, heldObject))
            {
                MoveObject(player2HoldPosition);
            }
        }
    }

    void findCenterOfPlayers()
    {

    }

    void MoveObject(Transform holdPosition)
    {

        if(player1Holding && player2Holding)
        {
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, player2.transform.position, 0.5f * Time.deltaTime);
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, player1.transform.position, 0.5f * Time.deltaTime);

            /*player1.transform.position = Vector3.Lerp(player1.transform.position, heldObject.transform.position, 1f * Time.deltaTime);
            player2.transform.position = Vector3.Lerp(player2.transform.position, heldObject.transform.position, 1f * Time.deltaTime);*/

        }
        else if (player1Holding)
        {
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, player1.transform.position, 0.001f * Time.deltaTime);
             
        }
        else if (player2Holding)
        {
            heldObject.transform.position = Vector3.Lerp(heldObject.transform.position, player2.transform.position, 0.001f * Time.deltaTime);
        }

    /*    if (Vector3.Distance(heldObject.transform.position, holdPosition.position) > 0.1f)
        {
            Vector3 moveDirection = (holdPosition.position - heldObject.transform.position);
            heldObject.GetComponent<Rigidbody>().AddForce(moveDirection * moveForce);
        }*/
    }


    bool isHoldingObject(GameObject player, GameObject obj)
    {
        if(player == player1 && player1Holding)
        {
            return true;
        }
        else if(player == player2 && player2Holding)
        {
            return true;
        }

        return false;
    }

    void PickupObject(GameObject pickObj, Transform holdPosition)
    {
        Debug.Log("Object Picked up");
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 10;
            objRig.transform.parent = holdPosition;
            heldObject = pickObj;
        }
    }

    void PickupObject(GameObject pickObj)
    {
        Debug.Log("Object Picked up");
        if (pickObj.GetComponent<Rigidbody>())
        {
            Rigidbody objRig = pickObj.GetComponent<Rigidbody>();
            objRig.useGravity = false;
            objRig.drag = 10;
            objRig.transform.parent = pickObj.transform;
            heldObject = pickObj;
        }
    }

    void DropObject()
    {
        Rigidbody heldRig = heldObject.GetComponent<Rigidbody>();
        heldRig.useGravity = true;
        heldRig.drag = 1;
        heldObject.transform.parent = null;
        heldObject = null;

    }
}
