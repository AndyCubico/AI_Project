using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum state { WANDER, CHASE };

public class zombieManager : MonoBehaviour
{
    public state zombieState = state.WANDER;
    //spawn info
    [Header("Zombie Spawn Settings")]
    public static zombieManager myManager;
    public GameObject zombiePrefab;
    public int numZombies = 5;
    public List<GameObject> allZombies;
    public Vector3 limits = new Vector3(5, 5, 5);

    //chase target
    public Transform target;

    //settings zombies
    [Header("Zombie Wander Settings")]
    [Range(0.0f,10.0f)]
    public float radius = 10.0f;
    [Range(0.0f, 10.0f)]
    public float offset = 2.0f;
    [Range(0.0f, 10.0f)]
    public float freqMaxWander = 2.5f;

    //chase settings
    [Header("Zombie Wander Settings")]
    [Range(0.0f, 10.0f)]
    public float freqMaxChase = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        myManager = this;

        StartCoroutine(spawnZombies());
    }

    // Update is called once per frame
    void Update()
    {
        switch (zombieState)
        {
            case state.WANDER:
                BroadcastMessage("wander", SendMessageOptions.DontRequireReceiver);
                break;
            case state.CHASE:
                BroadcastMessage("chase", SendMessageOptions.DontRequireReceiver);
                break;
            default:
                break;
        }
    }

    IEnumerator spawnZombies()
    {
        for (int i = 0; i < numZombies; ++i)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-limits.x, limits.x),
                1, Random.Range(-limits.z, limits.z)); // random position
            allZombies.Add((GameObject)Instantiate(zombiePrefab, pos, Quaternion.identity, GameObject.Find("zombieManager").transform));
            yield return new WaitForSeconds(0.5f);
        }
    }
}
