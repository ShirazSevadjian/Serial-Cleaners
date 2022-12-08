using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarFieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    private float searchInterval;

    public GameObject player1;
    public GameObject player2;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public AudioSource carHorn;

    public bool spottedPlayer;

    private bool searchForPlayers;
    private bool sirenEnabled = false;

    private Coroutine searchRoutine;

    public NavMeshAgent navAgent;

    public float timeLoss = 3f;

    private bool substractTime = false;
    private bool isRunningPauseAgent = false;

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
            if (!sirenEnabled)
            {
                PlayHorn();
            }

            if (!substractTime)
            {
                LoseTime();
            }

            if (!isRunningPauseAgent)
            {
                StartCoroutine(PauseAgent());
                isRunningPauseAgent = true;
            }
            

           
        }
    }

    private IEnumerator PauseAgent()
    {
        navAgent.isStopped = true;
       
        yield return new WaitForSecondsRealtime(10);

        navAgent.isStopped = false;
        sirenEnabled = false;
        substractTime = false;
        isRunningPauseAgent = false;
    }
    private IEnumerator Search()
    {
        //Will run constantly 
        while (searchForPlayers)
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
            if (collider.Length == 1)
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
        else if (spottedPlayer)
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

    private void PlayHorn()
    {
        sirenEnabled = true;
        carHorn.Play();
    }

    private void LoseTime()
    {
        substractTime = true;
        LevelManager.Instance.SubstractTime(timeLoss);
    }
}
