using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject melee;
    //private GameObject ranger;

    public float meleeInterval = 1f;
    //private float rangerInterval = 10f;

    public int enemyCounter = 0;
    public int limit;
    public int maxlimit;

    public Transform chosenPoint;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;

    public Dictionary<Transform, float> savedData = new Dictionary<Transform, float>();
    public Dictionary<Transform, float> bestData = new Dictionary<Transform, float>();

    public bool loop = true;

    private float min = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = 0;
        chosenPoint = point1;
        StartCoroutine(spawnEnemyStart(meleeInterval, melee, chosenPoint, 0));
    }

    private void Update()
    { 
        if (savedData.Count >= 4 && loop == true)
        {
            StopAllCoroutines();
            loop = false;
            StartCoroutine(SpawnEnemy(meleeInterval, melee));
        }
    }

    public IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        if (GameObject.FindGameObjectsWithTag("Melee").Length <= 0)
        {
            enemyCounter = 0;
            if (bestData[chosenPoint] > savedData[chosenPoint])
            {
                bestData[chosenPoint] = savedData[chosenPoint];
            }
            /*Debug.Log("Dictionary: " + point1.ToString() + ", " + savedData[point1]);
            Debug.Log("Dictionary: " + point2.ToString() + ", " + savedData[point2]);
            Debug.Log("Dictionary: " + point3.ToString() + ", " + savedData[point3]);
            Debug.Log("Dictionary: " + point4.ToString() + ", " + savedData[point4]);*/
        }

        yield return new WaitForSeconds(interval);

        if (enemyCounter < limit)
        {
            foreach (Transform t in savedData.Keys)
            {
                if (savedData[t] < min)
                {
                    min = savedData[t];
                    chosenPoint = t;
                }
            }
            GameObject newEnemy = Instantiate(enemy, new Vector3(chosenPoint.position.x, chosenPoint.position.y, chosenPoint.position.z), Quaternion.identity);
            enemyCounter++;
            min = 1000f;
            if (bestData[chosenPoint] < savedData[chosenPoint] && limit < maxlimit)
            {
                savedData[chosenPoint] = bestData[chosenPoint];
                Debug.Log("This is the best distance for: " + chosenPoint.ToString() + ", " + bestData[chosenPoint]);
                limit++;
            }
        }
        loop = true;
    }

    private IEnumerator spawnEnemyStart(float interval, GameObject enemy, Transform point, int num)
    {
        if (GameObject.FindGameObjectsWithTag("Melee").Length <= 0)
        {
            enemyCounter = 0;
        }

        yield return new WaitForSeconds(interval);

        if(enemyCounter < limit)
        {
            GameObject newEnemy = Instantiate(enemy, new Vector3(point.position.x, point.position.y, point.position.z), Quaternion.identity);
            enemyCounter++;
            num++;
        }
        yield return new WaitForSeconds(interval);
        
        switch (num)
        {
            case 1: chosenPoint = point2; 
                break;
            case 2: chosenPoint = point3; 
                break;
            case 3: chosenPoint = point4; 
                break;
            default: 
                break;
        }
        StartCoroutine(spawnEnemyStart(interval, enemy, chosenPoint, num));
    }
}
