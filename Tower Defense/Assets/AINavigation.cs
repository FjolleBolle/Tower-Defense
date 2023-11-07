using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject goal;


    private void Awake()
    {
        goal = GameObject.FindGameObjectWithTag("Base");
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.destination = goal.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
