using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public int damage;
    public MeleeEnemy melee;
    public GameObject meleeEnemy, rangerEnemy;

    public List<GameObject> targets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (melee.health == 0)
        {
            Destroy(meleeEnemy);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered!");
        if (other.gameObject.CompareTag("Melee") || other.gameObject.CompareTag("Ranger"))
        {
            targets.Add(other.gameObject);
            StartCoroutine(TickDamage());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Escaped!");
        if (other.gameObject.CompareTag("Melee") || other.gameObject.CompareTag("Ranger"))
        {
            targets.Remove(other.gameObject);
            StopAllCoroutines();
        }
    }

    private IEnumerator TickDamage()
    {
        Debug.Log("Attack");
        melee.health -= damage;
        yield return new WaitForSeconds(1f);
        StartCoroutine(TickDamage());
    }
}
