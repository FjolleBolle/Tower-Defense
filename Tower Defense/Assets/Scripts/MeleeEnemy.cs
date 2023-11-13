using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int health;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Range"))
        {
            StartCoroutine(TickDamage());
        }
    }

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Range"))
    //    {
    //        StopCoroutine(TickDamage());
    //    }
    //}

    IEnumerator TickDamage()
    {
        health = health - 10;
        yield return null;
    }
}
