using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombieMovement : MonoBehaviour
{
	// Raycast
	public Camera frustum;
	public LayerMask mask;

    //wander
    NavMeshAgent agent;
    private Vector3 worldTarget = Vector3.zero;
    private float freqWander = 0f;
	private float freqChase = 0f;

	private Animator zombieAnimator;

    void Start()
    {
		agent = GetComponent<NavMeshAgent>();
		frustum = gameObject.GetComponentInChildren<Camera>();
		mask = LayerMask.GetMask("target");
		zombieAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

		foreach (Collider col in colliders)
		{
			if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
			{
				RaycastHit hit;
				Ray ray = new Ray();
				ray.origin = transform.position;
				ray.direction = (col.transform.position - transform.position).normalized;
				ray.origin = ray.GetPoint(frustum.nearClipPlane);

				if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                {
					if (hit.collider.gameObject.CompareTag("target"))
					{
						zombieManager.myManager.zombieState = state.CHASE;
					}
				}
				else
				{
					zombieManager.myManager.zombieState = state.WANDER;
				}
			}
		}
	}

	void chase()
	{
        if (!zombieAnimator.GetBool("walk"))
        {
			zombieAnimator.SetBool("walk", true);
		}
		zombieAnimator.SetBool("run", true);

		freqChase += Time.deltaTime;

		if (freqChase > zombieManager.myManager.freqMaxChase)
        {
			freqChase -= zombieManager.myManager.freqMaxChase;
			agent.SetDestination(zombieManager.myManager.target.position);
		}
	}

	void wander()
    {
		zombieAnimator.SetBool("run", false);

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
			zombieAnimator.SetBool("walk", false);
		}
        else
        {
			zombieAnimator.SetBool("walk", true);
		}

		freqWander += Time.deltaTime;

        if (freqWander > zombieManager.myManager.freqMaxWander)
        {
			freqWander -= zombieManager.myManager.freqMaxWander;
            Vector3 localTarget = UnityEngine.Random.insideUnitCircle * zombieManager.myManager.radius;
            localTarget += new Vector3(0, 0, zombieManager.myManager.offset);
            worldTarget = transform.TransformPoint(localTarget);
            worldTarget.y = 0f;
        }

        agent.destination = worldTarget;
    }
}


