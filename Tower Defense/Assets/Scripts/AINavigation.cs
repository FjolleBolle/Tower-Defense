using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject goal;
    public GameObject navMesh;

    public GameObject costChangePrefab;
    public NavMeshSurface surface;

    public bool changeCost;

    private void Awake()
    {
        goal = GameObject.FindGameObjectWithTag("Base");
        navMesh = GameObject.FindGameObjectWithTag("Surface");
        surface = navMesh.GetComponent<NavMeshSurface>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        surface.BuildNavMesh();
    }

    // Start is called before the first frame update
    void Start()
    {
        changeCost = true;
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

    public IEnumerator PlaceCostField()
    {
        changeCost = false;
        Instantiate(costChangePrefab, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        surface.BuildNavMesh();
        Destroy(gameObject);
        //changeCost = true;
    }
}
