using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class flee : MonoBehaviour
{
    private NavMeshAgent _agent;
    [Range(0.0f, 10.0f)]
    public float zombieDistance = 4.0f;
    private Vector3 newPos = Vector3.zero;
    private Vector3 dirToZombie = Vector3.zero;
    private int groupSize = 0;
    private Animator targetAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        targetAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        groupSize = 0;

        foreach (GameObject go in zombieManager.myManager.allZombies)
        {
            float distance = Vector3.Distance(transform.position, go.transform.position);

            if (distance <= zombieDistance)
            {
                dirToZombie += transform.position - go.transform.position;
                groupSize++;
            }
        }

        if (groupSize == 0)
        {
            targetAnimator.SetBool("walk", false);
            _agent.isStopped = true;
        }

        else
        {
            targetAnimator.SetBool("walk", true);
            _agent.isStopped = false;
            dirToZombie = dirToZombie.normalized;
            newPos = transform.position + dirToZombie;
            _agent.SetDestination(newPos);
        }
    }
}
