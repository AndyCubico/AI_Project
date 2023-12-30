using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;


public class patrol : MonoBehaviour
{
    public Transform[] points;
    public int destPoint = 0;
    private NavMeshAgent agent;
    private float freq = 0f;
    [SerializeField] float freqMax = 2.5f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        if (SpawnRand.mySpawner.randomOrder == 0)
        {
            System.Array.Reverse(points);
        }

        GotoNextPoint();
    }

    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            freq += Time.deltaTime;

            if (freq > freqMax)
            {
                freq -= freqMax;
                GotoNextPoint();
            }           
        }        
    }
    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }
}
