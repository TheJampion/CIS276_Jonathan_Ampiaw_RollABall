using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRigidbodyController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 input;
    private float speed = 3f;
    private bool jumping;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Jump()
    {
        if (jumping)
        {
            jumping = false;
            rb.AddForce(speed * Vector3.up, ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = (rb.velocity.y * Vector3.up) + (input * speed);
        Jump();
    }

    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }
    }
}
