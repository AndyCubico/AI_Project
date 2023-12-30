using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FiniteStateMachine : MonoBehaviour
{
    public Transform cop;
    public GameObject target;
    public float dist2Steal = 10f;
    movement move;
    NavMeshAgent agent;

    private WaitForSeconds wait = new WaitForSeconds(0.05f);
    delegate IEnumerator State();
    private State state;
    public bool spoted = false;

    IEnumerator Start()
    {
        move = gameObject.GetComponent<movement>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        yield return wait;

        state = Wander;

        while (enabled)
            yield return StartCoroutine(state());
    }

    IEnumerator Wander()
    {
        Debug.Log("Wander state");

        while (spoted)
        {
            move.wander();
            yield return wait;
        };

        state = Approaching;
    }

    IEnumerator Approaching()
    {
        Debug.Log("Approaching state");

        agent.speed = 2f;
        move.seek(target.transform.position);

        bool stolen = false;
        while (!spoted)
        {
            if (Vector3.Distance(target.transform.position, transform.position) < 2f)
            {
                stolen = true;
                break;
            };
            yield return wait;
        };

        if (stolen)
        {
            target.SetActive(false);
            agent.speed *= 3;
            Debug.Log("Stolen");
            state = Hiding;
        }
        else
        {
            agent.speed = 1f;
            state = Wander;
        }
    }


    IEnumerator Hiding()
    {
        Debug.Log("Hiding state");

        while (true)
        {
            move.hide();
            yield return wait;
        };
    }
}
