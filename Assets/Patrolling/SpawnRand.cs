using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRand : MonoBehaviour
{
    public Transform[] points;

    public GameObject prefab;
    public GameObject ghost;

    public static SpawnRand mySpawner;//access with other scripts
    public int randomOrder;//agent direction

    // Start is called before the first frame update
    void Start()
    {
        randomOrder = Random.Range(0, 2);

        if (randomOrder == 0)
        {
            System.Array.Reverse(points);
        }

        int i = Random.Range(0, points.Length);

        prefab.SetActive(true);
        prefab.transform.position = points[i].position;

        int j = i + 1;

        if (j >= points.Length)
        {
            j = 0;
        }

        ghost.GetComponent<patrol>().destPoint = j;

        Vector3 direction = points[j].position - prefab.transform.position;
        prefab.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 1);

        mySpawner = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
