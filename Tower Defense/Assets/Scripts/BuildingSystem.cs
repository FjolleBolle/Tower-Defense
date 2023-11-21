using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingSystem : MonoBehaviour
{
    public LayerMask GroundLayerMask;


    private GameObject _buildingPrefab;
    private GameObject _toBuild;


    private Camera _mainCamera;

    private Ray _ray;
    private RaycastHit _hit;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _buildingPrefab = null;
       
    }

    private void Update()
    {
        if (_buildingPrefab != null)
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                if(_toBuild.activeSelf) _toBuild.SetActive(false);
            }
            else if (!_toBuild.activeSelf)
            {
                _toBuild.SetActive(true);
            }




            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit, 1000f, GroundLayerMask))
            {
                if (!_toBuild.activeSelf) _toBuild.SetActive(true);

                _toBuild.transform.position = _hit.point;

                if (Input.GetMouseButtonDown(0))
                {
                    tower_Manager m = _toBuild.GetComponent <tower_Manager>();
                    if (m.ValidPlacement == true)
                    {

                        if (_toBuild.gameObject.tag == "RangeTower") {
                            _toBuild.GetComponentInChildren<CapsuleCollider>().enabled = true;
                            _toBuild.GetComponentInChildren<SphereCollider>().enabled = true;
                        }

                        _buildingPrefab = null;
                        _toBuild = null;
                    }


                    
                }
            }
            else if (_toBuild.activeSelf) _toBuild.SetActive(false);
        }
    }


    public void SetBuildingPrefab(GameObject prefab)
    {
        _buildingPrefab = prefab;
        _PrepareBuilding();
    }

    private void _PrepareBuilding()
    {
        if (_toBuild)
        {
            Destroy(_toBuild);
        }


        _toBuild = Instantiate(_buildingPrefab);
        _toBuild.SetActive(false);
    }

}
