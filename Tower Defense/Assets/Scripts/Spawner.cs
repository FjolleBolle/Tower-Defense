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

    // Start is called before the first frame update
    void Start()
    {
        enemyCounter = 0;
        StartCoroutine(spawnEnemy(meleeInterval, melee, chosenPoint));
        //StartCoroutine(spawnEnemy(rangerInterval, ranger));
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
        }

        point = chosenPoint;

        if(enemyCounter < 5)
        {
            GameObject newEnemy = Instantiate(enemy, new Vector3(point.position.x, point.position.y, point.position.z), Quaternion.identity);
            enemyCounter++;
        }

        StartCoroutine(spawnEnemy(interval, enemy, point));
    }
}
