using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int health = 10;
    public int currentHp;
    public int damage;

    private Spawner spawner;
    public float timeAlive;
    public Transform spawnPoint;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        spawnPoint = spawner.chosenPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
        {
            timeAlive += Time.deltaTime;

        }

        if (health <= 0 && GetComponent<AINavigation>().changeCost)
        {
            if (spawner.savedData.ContainsKey(spawnPoint)) //Vi skal også tjekke om timeAlive er større end den værdi der allerede er der
            {
                spawner.savedData[spawnPoint] = timeAlive;
            }
            else
            {
                spawner.savedData.Add(spawnPoint, timeAlive);  
            }
            StartCoroutine(GetComponent<AINavigation>().PlaceCostField());
        }
    }
}
