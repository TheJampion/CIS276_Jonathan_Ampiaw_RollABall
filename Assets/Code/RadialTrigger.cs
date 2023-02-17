using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class RadialTrigger : MonoBehaviour
{
    [SerializeField]
    private float radius = 4f;
    private float distance;
    [SerializeField]
    private Transform objectB;
    private void OnDrawGizmos()
    {
        distance = (transform.position - objectB.transform.position).magnitude;
        //Calculate whether your in the radius or not
        if(distance <= radius)
        {
            Handles.color = Color.green;
        }
        else
        {
            Handles.color = Color.red;
        }
        Debug.Log(distance);
        Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
    }


}
