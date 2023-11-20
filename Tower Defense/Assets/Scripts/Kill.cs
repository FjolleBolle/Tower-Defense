using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered!");
        if (other.gameObject.CompareTag("Melee") || other.gameObject.CompareTag("Ranger"))
        {
            Destroy(other.gameObject);
        }
    }
}