using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    //Assigned in Inspector
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private LayerMask groundCollisionLayerMask;
    [SerializeField]
    private Transform grabPoint;
    [SerializeField]
    private GameObject grabbableBox;

    //Variables
    private Rigidbody rb;
    private Vector3 input;
    private bool jumping;
    private float distanceToGround;
    private Grabbable selectedGrabbable;

    private void OnDrawGizmos()
    {
        Color raycastColor;

        if (IsGrounded())
        {
            raycastColor = Color.green;
        }
        else
        {
            raycastColor = Color.red;
        }
        Vector3 startPosition = transform.position + (distanceToGround - 0.05f) * Vector3.down;
        Vector3 endPosition = startPosition + 0.3f * Vector3.down;
        Debug.DrawLine(startPosition, endPosition, raycastColor);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }
    private bool IsGrounded()
    {
        Vector3 startPosition = transform.position + (distanceToGround - 0.05f) * Vector3.down;
        return Physics.Raycast(startPosition, Vector3.down, 0.3f, groundCollisionLayerMask);
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
        if (input.magnitude != 0)
        {
            transform.forward = input;
        }
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumping = true;
        }
        if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position, 1f, transform.forward, out RaycastHit hitInfo, maxDistance: 1f, preview: RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Editor) 
            && hitInfo.transform.TryGetComponent(out Grabbable grabbable)
            && Input.GetKey(KeyCode.Z)
            && selectedGrabbable == null)
        {
            selectedGrabbable = grabbable;
            grabbable.Grab(grabPoint);
        }
        if (Input.GetKeyDown(KeyCode.C) && selectedGrabbable)
        {
            selectedGrabbable.Drop();
            selectedGrabbable = null;
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject builtBox = Instantiate(grabbableBox, transform.position + (1 * transform.forward), Quaternion.identity);
            if(builtBox.TryGetComponent(out Rigidbody rb))
            {
                rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
            }
        }
    }
}
