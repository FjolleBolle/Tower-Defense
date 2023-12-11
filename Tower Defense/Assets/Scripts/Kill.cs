using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public int damage;

    public List<GameObject> targets = new List<GameObject>();

    public float time = 1f;

    public float timer = 0f;

    private void Start()
    {
        timer = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if(targets.Count > 0)
        {
            timer += Time.deltaTime; 
            if (targets[0].GetComponent<MeleeEnemy>().health <= 0)
            {
                targets.RemoveAt(0);
            }
        }
        else if(targets.Count <= 0)
        {
            timer = 0.4f;
        }

        if (timer >= time)
        {
            timer = 0f;
            Debug.Log("Attack");
            targets[0].GetComponent<MeleeEnemy>().health -= damage;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered!");
        if (other.gameObject.CompareTag("Melee") || other.gameObject.CompareTag("Ranger"))
        {
            targets.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Escaped!");
        if (other.gameObject.CompareTag("Melee") || other.gameObject.CompareTag("Ranger"))
        {
            targets.Remove(other.gameObject);
        }
    }

    /*private IEnumerator TickDamage()
    {
        Debug.Log("Attack");
        targets[0].GetComponent<MeleeEnemy>().health -= damage;
        yield return new WaitForSeconds(time);
        StartCoroutine(TickDamage());
    }*/
}
