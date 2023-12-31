using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction



namespace BBUnity.Actions
{
    [Action("Run away")]
    public class RunAway : GOAction
    {
        [InParam("Flowy1")]
        private GameObject flowy1;
        [InParam("Flowy2")]
        private GameObject flowy2;
        [InParam("DistEnemy")]
        private float distEnemy;

        [InParam("target")]
        [Help("Target position where the game object will be moved")]
        public Vector3 target;

        private Vector3 newPos = Vector3.zero;
        private Vector3 dirToZombie = Vector3.zero;
        private int groupSize = 0;
        private List<GameObject> enemies;
        private UnityEngine.AI.NavMeshAgent navAgent;

        public override void OnStart()
        {
            navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (navAgent == null)
            {
                Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
                navAgent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
            }
            navAgent.SetDestination(target);

            enemies = zombieManager.myManager.allZombies;
            enemies.Add(flowy1);
            enemies.Add(flowy2);

            #if UNITY_5_6_OR_NEWER
                        navAgent.isStopped = false;
            #else
                            navAgent.Resume();
            #endif

        }

        public override TaskStatus OnUpdate()
        {
            groupSize = 0;

            foreach (GameObject go in enemies)
            {
                float distance = Vector3.Distance(gameObject.transform.position, go.transform.position);

                if (distance <= distEnemy)
                {
                    dirToZombie += gameObject.transform.position - go.transform.position;
                    groupSize++;
                }
            }

            if (groupSize != 0)
            {
                dirToZombie = dirToZombie.normalized;
                newPos = gameObject.transform.position + dirToZombie;
                navAgent.speed = 30.0f;
                navAgent.SetDestination(newPos);
            }

            else 
            {
                navAgent.speed = 7.5f;
                navAgent.SetDestination(target);
            }

            //if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
            //    return TaskStatus.COMPLETED;

            return TaskStatus.RUNNING;
        
        }
    }
}