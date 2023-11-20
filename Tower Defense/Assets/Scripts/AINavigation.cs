using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject goal;

    public GameObject costChangePrefab;
    public NavMeshSurface surface;

    public bool changeCost = false;

    private void Awake()
    {
        goal = GameObject.FindGameObjectWithTag("Base");
        agent = gameObject.GetComponent<NavMeshAgent>();
        surface.BuildNavMesh();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent.destination = goal.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && changeCost == true)
        {
            changeCost = false;
            StartCoroutine(PlaceCostField());
        }
    }

    IEnumerator PlaceCostField()
    {
        Instantiate(costChangePrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        surface.BuildNavMesh();
        changeCost = true;
    }
}
