using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacementController2 : MonoBehaviour
{
    //We should be able to take our mouse, move it over the terrain, and select from a group of 3 different objects, and place them based on our mouse position on the terrain.
    //The placement will be based off of where our mouse meets the terrain in our camera view
    //The placement should only be able to happen on the terrain, not on other game objects
    //Assigned in Inspector
    [SerializeField]
    private GameObject[] placeableObjects;
    [SerializeField]
    private LayerMask layerMask;
    
    //Variables
    private Camera camera;
    private GameObject currentBuildableObject;
    private bool isBuilding;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (currentBuildableObject == null)
            {
                currentBuildableObject = Instantiate(placeableObjects[0]);
            }
            if (isBuilding)
            {
                if (currentBuildableObject.TryGetComponent(out Buildable2 buildable))
                {
                    buildable.Build();
                    currentBuildableObject = null;
                }
            }
            isBuilding = !isBuilding;
        }
        if (!isBuilding) return;

        MoveBuildableObject();
    }

    private void MoveBuildableObject()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
        {
            if (currentBuildableObject.TryGetComponent(out Buildable2 buildable))
            {
                currentBuildableObject.transform.position = hitInfo.point + buildable.heightOffset * Vector3.up;
            }
        }
    }
}
