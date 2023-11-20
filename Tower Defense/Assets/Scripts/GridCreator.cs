using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public int columnLength;
    public int rowLength;

    public float x_Space;
    public float z_Space;

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < columnLength * rowLength; i++)
        {
            Instantiate(prefab, new Vector3(x_Space + (x_Space * (i % columnLength)), 0, z_Space + (z_Space * (i / columnLength))), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
