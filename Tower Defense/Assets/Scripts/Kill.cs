using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public int damage;

    public List<GameObject> targets = new List<GameObject>();

    public float time = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targets.Count > 0)
        {
            if (targets[0].GetComponent<MeleeEnemy>().health <= 0)
            {
                targets.RemoveAt(0);
            }
        }
        
        if(targets.Count == 0)
        {
            StopAllCoroutines();
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
        targets[0].GetComponent<MeleeEnemy>().health -= damage;
        yield return new WaitForSeconds(time);
        StartCoroutine(TickDamage());
    }
}
