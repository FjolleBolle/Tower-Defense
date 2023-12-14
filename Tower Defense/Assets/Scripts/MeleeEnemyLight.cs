using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyLight : MonoBehaviour
{
    public int health = 10;
    public int currentHp;
    public int damage;

    private SpawnerLite spawner;
    public float timeAlive;
    public Transform spawnPoint;
    NavMeshAgent agent;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerLite>();
        spawnPoint = spawner.chosenPoint;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && GetComponent<AINavigation>().changeCost)
        {
            StartCoroutine(GetComponent<AINavigation>().PlaceCostField());
        }
    }
}
