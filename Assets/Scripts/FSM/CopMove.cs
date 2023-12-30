using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopMove : MonoBehaviour
{
    private float freqWander = 0.0f;
    private NavMeshAgent agent;
    private Vector3 worldTarget = Vector3.zero;

    [Header("Wander Settings")]
    [Range(0.0f, 10.0f)]
    public float freqMaxWander = 2.5f;
    [Range(0.0f, 50.0f)]
    public float radius = 5.0f;
    [Range(0.0f, 10.0f)]
    public float offset = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        freqWander += Time.deltaTime;

        if (freqWander > freqMaxWander)
        {
            freqWander -= freqMaxWander;
            Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
            localTarget += new Vector3(0, 0, offset);
            worldTarget = transform.TransformPoint(localTarget);
            worldTarget.y = 0f;
        }

        agent.destination = worldTarget;
    }
}
