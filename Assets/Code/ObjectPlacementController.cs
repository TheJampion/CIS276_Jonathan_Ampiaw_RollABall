using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacementController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] placeableObject;
    private int buildIndex = 0;
    private bool isBuilding;
    private Camera cam;
    [SerializeField]
    private LayerMask mask;
    private GameObject currentSelectedBuildable;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isBuilding = !isBuilding;
        }
        if (!isBuilding)
        { 
            if(currentSelectedBuildable != null)
            {
                PlaceBuildable();
                currentSelectedBuildable = null;
            }
            return;
        }
        SpawnBuildableObject();

        MoveBuildable();
    }

    private void SpawnBuildableObject()
    {
        if(currentSelectedBuildable == null)
        {
            currentSelectedBuildable = Instantiate(placeableObject[buildIndex]);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            buildIndex--;
            buildIndex = Mathf.Clamp(buildIndex, 0, 2);
            if (currentSelectedBuildable != null)
            {
                Destroy(currentSelectedBuildable);
                currentSelectedBuildable = Instantiate(placeableObject[buildIndex]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            buildIndex++;
            buildIndex = Mathf.Clamp(buildIndex, 0, 2);
            if (currentSelectedBuildable != null)
            {
                Destroy(currentSelectedBuildable);
                currentSelectedBuildable = Instantiate(placeableObject[buildIndex]);
            }
        }
    }

    private void MoveBuildable()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (currentSelectedBuildable == null) return;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, mask) && currentSelectedBuildable.TryGetComponent(out Buildable buildable))
        {
            currentSelectedBuildable.transform.position = hitInfo.point + buildable.heightOffset * Vector3.up;
        }
    }

    private void PlaceBuildable()
    {
        if (currentSelectedBuildable.TryGetComponent(out Buildable buildable))
        {
            buildable.Build();
        }
    }
}
