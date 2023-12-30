using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Follower : MonoBehaviour
{
    private NavMeshAgent agent;
    private float freq = 0f;
    [SerializeField] float freqMax = 2.5f;

    public GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        freq += Time.deltaTime;

        if (freq > freqMax)
        {
            freq -= freqMax;
            agent.destination = ghost.transform.position;
            Vector3 direction = ghost.GetComponent<NavMeshAgent>().destination - ghost.transform.position;
            agent.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime);
        }
    }
}
