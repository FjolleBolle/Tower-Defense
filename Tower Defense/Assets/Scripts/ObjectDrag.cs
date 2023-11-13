using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;


    private void OnMouseDown()
    {
        offset = transform.position - BuildingScript.getMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        Vector3 pos = BuildingScript.getMouseWorldPosition() + offset;
        transform.position = BuildingScript.current.SnapCoordinateToGrid(pos);
    }

}
