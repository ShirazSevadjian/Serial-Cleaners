using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Inspired by: https://github.com/Comp3interactive/FieldOfView


public class PoliceFieldOfView : MonoBehaviour
{

    public float radius;
    [Range(0, 360)]
    public float angle;

    private float searchInterval;

    public float lightSwitchTime = 0f;
    public float lightOnTime = 0.2f;
    public float lightOffTime = 0.2f;

    public GameObject player1;
    public GameObject player2;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public AudioSource policeSiren;

    public Light blueSiren;
    public Light redSiren;

    public bool spottedPlayer;

    private bool searchForPlayers;
    private bool sirenEnabled = false;

    private Coroutine searchRoutine;

    public NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        searchInterval = 0.2f;
        searchForPlayers = true;

        //Start searching for the players
        searchRoutine = StartCoroutine(Search());
    }

    // Update is called once per frame
    void Update()
    {
        //A player was spotted!
        if (spottedPlayer)
        {
            navAgent.isStopped = true;
            TurnPoliceLightsOn();

            if(!sirenEnabled)
            {
                EnableSiren();
            }

        }
    }


    private IEnumerator Search()
    {
        //Will run constantly 
        while(searchForPlayers)
        {
            yield return new WaitForSeconds(searchInterval);
            CheckFieldOfView();

            if (spottedPlayer)
            {
                searchForPlayers = false;
            }
        }
    }

    private void CheckFieldOfView()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (collider.Length != 0)
        {
            if(collider.Length == 1)
            {
                Transform target = collider[0].transform;
                spottedPlayer = CheckAngle(target);
            }
            else
            {
                foreach (Collider c in collider)
                {
                    Transform target = c.transform;

                    spottedPlayer = CheckAngle(target);
                }
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


    private void TurnPoliceLightsOn()
    {
        if(Time.time > lightSwitchTime)
        {
            redSiren.enabled = !redSiren.enabled;

            if (redSiren.enabled)
            {
                blueSiren.enabled = false;
            }
            else
            {
                blueSiren.enabled = true;
            }

            //Red siren
            if (redSiren.enabled)
            {
                redSiren.intensity = 50f;
                lightSwitchTime = Time.time + lightOnTime;
            }
            else
            {
                lightSwitchTime = Time.time + lightOffTime;
            }

            //Blue siren
            if (blueSiren.enabled)
            {
                blueSiren.intensity = 50f;
                lightSwitchTime = Time.time + lightOnTime;
            }
            else
            {
                lightSwitchTime = Time.time + lightOffTime;
            }
        }
    }

    private void EnableSiren()
    {
        sirenEnabled = true;
        policeSiren.loop = true;
        policeSiren.Play();
    }
}
