using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Rigidbody rb;
    private Collider collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
    public void Grab(Transform newParent)
    {
        transform.parent = newParent;
        transform.position = newParent.position;
        collider.enabled = false;
        rb.isKinematic = true;
    }

    public void Drop()
    {
        transform.parent = null;
        rb.isKinematic = false;
        collider.enabled = true;
    }
}
