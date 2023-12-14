using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    public int health = 10;
    public int currentHp;
    public int damage;

    private Spawner spawner;
    public float timeAlive;
    public Transform spawnPoint;
    NavMeshAgent agent;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        spawnPoint = spawner.chosenPoint;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 0)
        {
            timeAlive = RemainingDistance(agent.path.corners);
        }

        if (health <= 0 && GetComponent<AINavigation>().changeCost)
        {
            if (spawner.savedData.ContainsKey(spawnPoint)) //Vi skal også tjekke om timeAlive er større end den værdi der allerede er der
            {
                spawner.savedData[spawnPoint] = timeAlive;
                Debug.Log("Changed key");
            }
            else
            {
                spawner.savedData.Add(spawnPoint, timeAlive);
                Debug.Log("Added point!");
            }
            StartCoroutine(GetComponent<AINavigation>().PlaceCostField());
        }
    }

    public float RemainingDistance(Vector3[] points)
    {
        if (points.Length < 2) return 0;
        float distance = 0;
        for (int i = 0; i < points.Length - 1; i++)
            distance += Vector3.Distance(points[i], points[i + 1]);
        return distance;
    }
}
