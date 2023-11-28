using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject melee;
    //[SerializeField]
    //private GameObject ranger;

    [SerializeField] 
    private float meleeInterval = 5f;
    //[SerializeField]
    //private float rangerInterval = 10f;

    public int enemyCounter = 0;

    public Transform chosenPoint;
    public Transform point1;
    public Transform point2;

    public float timer;
    public List<float> prevTime;

    public bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = 0;
        prevTime = new List<float>();
        StartCoroutine(spawnEnemy(meleeInterval, melee, chosenPoint));
        //StartCoroutine(spawnEnemy(rangerInterval, ranger));
    }

    private void Update()
    {
        if (enemyCounter >= 1)
        {
            timer += Time.deltaTime;
        }
        else if(enemyCounter <= 0 && hasSpawned == true)
        {
            hasSpawned = false;
            prevTime.Add(timer);
            timer = 0;
        }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy, Transform point)
    {
        yield return new WaitForSeconds(interval);
        if (GameObject.FindGameObjectsWithTag("Melee").Length <= 0)
        {
            enemyCounter = 0;
            if (chosenPoint == point1)
            {
                chosenPoint = point2;
            }
            else if (chosenPoint == point2)
            {
                chosenPoint = point1;
            }
            yield return new WaitForSeconds(1);
        }

        point = chosenPoint;

        if(enemyCounter < 5)
        {
            hasSpawned = true;
            GameObject newEnemy = Instantiate(enemy, new Vector3(point.position.x, point.position.y, point.position.z), Quaternion.identity);
            enemyCounter++;
        }
        Debug.Log("Done");
        StartCoroutine(spawnEnemy(interval, enemy, point));
    }
}
