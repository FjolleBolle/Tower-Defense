using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;

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
        if (other.gameObject.CompareTag("Melee"))
        {
            Destroy(enemy1);
        }
        else if (other.gameObject.CompareTag("Ranger"))
        {
            Destroy(enemy2);
        }
    }
}
