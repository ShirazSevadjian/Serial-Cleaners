using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inspired by: https://github.com/Comp3interactive/FieldOfView


public class PoliceFieldOfView : MonoBehaviour
{

    public float radius;
    [Range(0, 360)]
    public float angle;

    private float searchInterval;

    public GameObject player1;
    public GameObject player2;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool spottedPlayer;

    private bool searchForPlayers;

    // Start is called before the first frame update
    void Start()
    {
        searchInterval = 0.2f;
        searchForPlayers = true;

        //Start searching for the players
        StartCoroutine(Search());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator Search()
    {
        //Will run constantly 
        while(searchForPlayers)
        {
            yield return new WaitForSeconds(searchInterval);
            CheckFieldOfView();
        }
    }

    private void CheckFieldOfView()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (collider.Length != 0)
        {
            foreach(Collider c in collider){
                Transform target = c.transform;

                spottedPlayer = CheckAngle(target);
            }

        }
        else if(spottedPlayer)
        {
            spottedPlayer = false;
        }
    }

    private bool CheckAngle(Transform target)
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        bool canSeePlayer = false;

        if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
        {
            //float distanceToTarget = Vector3.Distance(transform.position, target.position);

            //if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget))
            //{
            canSeePlayer = true;
            //}
        }
        else
        {
            canSeePlayer = false;
        }

        return canSeePlayer;
    }
}
