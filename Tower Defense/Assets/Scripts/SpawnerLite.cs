using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerLite : MonoBehaviour
{
    public GameObject melee;
    //private GameObject ranger;

    public float meleeInterval = 1f;
    //private float rangerInterval = 10f;

    public int enemyCounter = 0;
    public int limit;

    public Transform chosenPoint;
    public Transform point1;

    /*
    Vi laver en dictionary der kun har det antal entries der svarer til spawnpoints. Den gemmer kun den fjende der overlevede længst og ved hvilket spawnpoint. 
    Hvis der så kommer en der overlever længere ved det samme spawnpoint bliver entrien opdateret. Vi gider ikke gemme på alle fjender fordi vi vil kun have den "bedste". 
     */

    public bool hasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = 0;
        StartCoroutine(SpawnEnemy(meleeInterval, melee));
    }

    private void Update()
    {
        //if (savedData.Count >= 4 && spawning == true)
        //{
        //    StopAllCoroutines();
        //    spawning = false;
        //    StartCoroutine(SpawnEnemy(meleeInterval, melee));
        //}
    }

    public IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        if (GameObject.FindGameObjectsWithTag("Melee").Length <= 0 && hasSpawned == true)
        {
            enemyCounter = 0;
            hasSpawned = false;
        }

        if (enemyCounter < limit)
        {
            hasSpawned = true;

            GameObject newEnemy = Instantiate(enemy, new Vector3(chosenPoint.position.x, chosenPoint.position.y, chosenPoint.position.z), Quaternion.identity);
            enemyCounter++;
        }
        yield return new WaitForSeconds(interval);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }


    //private IEnumerator spawnEnemyStart(float interval, GameObject enemy, Transform point)
    //{
    //    if (GameObject.FindGameObjectsWithTag("Melee").Length <= 0)
    //    {
    //        enemyCounter = 0;
    //    }
    //    yield return new WaitForSeconds(interval);

    //    if (enemyCounter < limit)
    //    {
    //        GameObject newEnemy = Instantiate(enemy, new Vector3(point.position.x, point.position.y, point.position.z), Quaternion.identity);
    //        hasSpawned = true;
    //        enemyCounter++;
    //    }
    //    yield return new WaitForSeconds(interval);

    //    StartCoroutine(spawnEnemyStart(interval, enemy, chosenPoint));
    //}
}
