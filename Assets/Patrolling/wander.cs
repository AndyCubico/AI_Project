using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class wander : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] float radius = 10.0f;
    [SerializeField] float offset = 2.0f;
    private Vector3 worldTarget = Vector3.zero;
    private float freq = 0f;
    [SerializeField] float freqMax = 5.0f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Wander();
    }

    // Update is called once per frame
    void Update()
    {
        freq += Time.deltaTime;

        if (freq > freqMax)
        {
            freq -= freqMax;
            Wander();
        }
        
        if (agent.velocity.magnitude < 0.1f)
        {
            animator.SetBool("isWalking", false);
        }

        else
        {
            animator.SetBool("isWalking", true);
        }

        agent.destination = worldTarget;
    }

    void Wander()
    {
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);
        worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;
    }
}