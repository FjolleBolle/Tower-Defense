using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject melee;
    //[SerializeField]
    //private GameObject ranger;

    public float meleeInterval = 5f;
    //[SerializeField]
    //private float rangerInterval = 10f;

    public int enemyCounter = 0;
    public int limit;

    public Transform chosenPoint;
    public Transform point1;
    public Transform point2;

    public Dictionary<Transform, float> savedData = new Dictionary<Transform, float>();

    /*
    Vi laver en dictionary der kun har det antal entries der svarer til spawnpoints. Den gemmer kun den fjende der overlevede længst og ved hvilket spawnpoint. 
    Hvis der så kommer en der overlever længere ved det samme spawnpoint bliver entrien opdateret. Vi gider ikke gemme på alle fjender fordi vi vil kun have den "bedste". 
     */

    public bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = 0;
        chosenPoint = point1;
        StartCoroutine(spawnEnemy(meleeInterval, melee, chosenPoint));
        //StartCoroutine(spawnEnemy(rangerInterval, ranger));
    }

    private void Update()
    {
        if(enemyCounter <= 0 && hasSpawned == true)
        {
            hasSpawned = false;
            Debug.Log("Dictionary: " + savedData.Keys.Count + ", " + savedData.Values.Count);
        }
    }



    private IEnumerator spawnEnemy(float interval, GameObject enemy, Transform point)
    {
        if (GameObject.FindGameObjectsWithTag("Melee").Length <= 0)
        {
            enemyCounter = 0;
        }
        yield return new WaitForSeconds(interval);

        point = chosenPoint;

        if(enemyCounter < limit)
        {
            GameObject newEnemy = Instantiate(enemy, new Vector3(point.position.x, point.position.y, point.position.z), Quaternion.identity);
            hasSpawned = true;
            enemyCounter++;
        }
        StartCoroutine(spawnEnemy(interval, enemy, point));
    }
}
