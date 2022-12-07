using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolCar : MonoBehaviour
{
    [SerializeField]
    public NavMeshAgent agent;
    public Transform[] targets;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Start moving the agent to the first position
        agent.destination = targets[i].position;
    }

    // Update is called once per frame
    void Update()
    {
        StartPatrol();
    }


    private void StartPatrol()
    {
        var distance = Vector3.Distance(targets[i].position, agent.transform.position);

        //Check if within 10 units of the target position
        if (distance < 10)
        {
            //Increment target position
            i++;
            
            //If there are still targets remaining
            if(i < targets.Length)
            {
                //Go to the next target
                agent.destination = (targets[i].position);
            }

        }

        //If we went to all the targets, reset it to the initial one to restart the patrol
        if (i == targets.Length)
        {
            i = 0;
            agent.destination = (targets[i].position);
        }
    }
}
