using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Tilemaps;

public class BuildingScript : MonoBehaviour
{

    public static BuildingScript current;

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private TileBase WhiteTile;

    public GameObject Prefab1;

    public GameObject Prefab2;

    private PlaceableObject ObjectToPlace;

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            InitializeWithObject(Prefab1);
        }
        else if(Input.GetKeyDown(KeyCode.B)) 
        {
            InitializeWithObject(Prefab2);
        }


        if (!ObjectToPlace)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log("working 0");
            if (CanBePlaced(ObjectToPlace))
            {
                Debug.Log("working 1");
                ObjectToPlace.Place();
                Vector3Int start = gridLayout.WorldToCell(ObjectToPlace.GetStartPosition());
                TakeArea(start, ObjectToPlace.Size);
            }
            else
            {
                Destroy(ObjectToPlace.gameObject);
            } 
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(ObjectToPlace.gameObject);

        }



    }



    public static Vector3 getMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        { 
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }


    private static TileBase[] GetTileBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, z:0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }




    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        obj.AddComponent<ObjectDrag>();
    }


    private bool CanBePlaced(PlaceableObject placeableObejct)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(ObjectToPlace.GetStartPosition());
        area.size = placeableObejct.Size;

        TileBase[] baseArray = GetTileBlock(area, MainTilemap);


        foreach (var B in baseArray)
        {
            if (B == WhiteTile)
            {
                return false;
            }
        }
        return true;

    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        MainTilemap.BoxFill(start, WhiteTile, start.x, start.y, start.x + size.x, start.y + size.y);
    }

  
}
