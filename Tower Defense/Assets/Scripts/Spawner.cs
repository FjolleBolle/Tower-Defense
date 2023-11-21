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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(meleeInterval, melee));
        //StartCoroutine(spawnEnemy(rangerInterval, ranger));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
