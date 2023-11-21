using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_Manager : MonoBehaviour
{
    private int _nObjects;

    public bool ValidPlacement;

    private void Awake()
    {
        _nObjects = 0;
        ValidPlacement = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _nObjects++;
        if (_nObjects <= 1) { ValidPlacement = false; }
    }

    private void OnTriggerExit(Collider other)
    {
        _nObjects--;

        if (_nObjects == 0 ) { ValidPlacement = true; }
    }
}
