using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Mesh;

public class MeleeEnemy : MonoBehaviour
{
    public int health = 10;
    public int currentHp;
    public int damage;

    private Spawner spawner;
    public float distLeft;
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
            distLeft = RemainingDistance(agent.path.corners);
        }

        if (health <= 0 && GetComponent<AINavigation>().changeCost)
        {
            if (spawner.savedData.ContainsKey(spawnPoint)) //Vi skal ogs� tjekke om timeAlive er st�rre end den v�rdi der allerede er der
            {
                spawner.savedData[spawnPoint] = distLeft;
                Debug.Log("Changed key");
            }
            else
            {
                spawner.savedData.Add(spawnPoint, distLeft);
                spawner.bestData.Add(spawnPoint, distLeft);
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
