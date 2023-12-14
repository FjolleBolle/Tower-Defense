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

    public Transform chosenPoint;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;

    public Dictionary<Transform, float> savedData = new Dictionary<Transform, float>();

    /*
    Vi laver en dictionary der kun har det antal entries der svarer til spawnpoints. Den gemmer kun den fjende der overlevede længst og ved hvilket spawnpoint. 
    Hvis der så kommer en der overlever længere ved det samme spawnpoint bliver entrien opdateret. Vi gider ikke gemme på alle fjender fordi vi vil kun have den "bedste". 
     */

    public bool hasSpawned = false;
    public bool spawning = true;

    private float min = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = 0;
        StartCoroutine(spawnEnemyStart(meleeInterval, melee, point1, 0));
    }

    private void Update()
    { 
        if (savedData.Count >= 4 && spawning == true)
        {
            StopAllCoroutines();
            spawning = false;
            StartCoroutine(SpawnEnemy(meleeInterval, melee));
        }
    }

    public IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        if (GameObject.FindGameObjectsWithTag("Melee").Length <= 0 && hasSpawned == true)
        {
            enemyCounter = 0;
            hasSpawned = false;
            Debug.Log("Dictionary: " + savedData.ContainsKey(point1) + ", " + savedData[point1]);
            Debug.Log("Dictionary: " + savedData.ContainsKey(point2) + ", " + savedData[point2]);
            Debug.Log("Dictionary: " + savedData.ContainsKey(point3) + ", " + savedData[point3]);
            Debug.Log("Dictionary: " + savedData.ContainsKey(point4) + ", " + savedData[point4]);
        }

        if (enemyCounter < limit)
        {
            hasSpawned = true;
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
        }
        yield return new WaitForSeconds(interval);
        spawning = true;
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
            hasSpawned = true;
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
