using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
	public FiniteStateMachine thiefFNM;

	// Raycast
	public Camera frustum;
	public LayerMask mask;

	// Start is called before the first frame update
	void Start()
    {
		frustum = gameObject.GetComponentInChildren<Camera>();
		mask = LayerMask.GetMask("thief");
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
					if (hit.collider.gameObject.CompareTag("thief"))
					{
						thiefFNM.spoted = true;
					}
				}
				//thiefFNM.spoted = true;
			}
			else
			{
				thiefFNM.spoted = false;
			}
		}
	}
}
