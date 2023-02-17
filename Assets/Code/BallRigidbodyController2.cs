using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRigidbodyController2 : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float turnSpeed = 30f;
    private Rigidbody rb;
    private Vector3 input;
    private bool isJumping;
    private float score;

    private float distToGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position + distToGround * Vector3.down, transform.position + (distToGround + 0.1f) * Vector3.down);
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            score++;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = (rb.velocity.y * Vector3.up) + (input * speed);
        if (isJumping)
        {
            isJumping = false;
            rb.AddForce(speed * Vector3.up, ForceMode.Impulse);
        }
    }
    void Update()
    {
        Debug.Log(IsGrounded());
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (input.magnitude != 0)
        {
            transform.forward = input;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
    }
}
