using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int health = 10;
    public int currentHp;
    public int damage;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && GetComponent<AINavigation>().changeCost)
        {
            StartCoroutine(GetComponent<AINavigation>().PlaceCostField());
        }
    }
}
