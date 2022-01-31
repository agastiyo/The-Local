using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Range(10f,20f)]
    public float movementSpeed;
    [Range(0f,1f)]
    public float jumpForce;
    [Range(1f,2f)]
    public float sprintMultiplier;

    private float moveSpeedTemp;
    private float jumpRaycastDistance = 1.05f;

    private Rigidbody rb;

    private void Start() { rb = GetComponent<Rigidbody>(); }

    private void Update() { Jump(); }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (_sprint()) { moveSpeedTemp = movementSpeed * sprintMultiplier; }
        else { moveSpeedTemp = movementSpeed; }

        float hAxis = moveSpeedTemp * Input.GetAxis("Horizontal") * Time.deltaTime;
        float vAxis = moveSpeedTemp * Input.GetAxis("Vertical") * Time.deltaTime;

        Vector3 movement = new Vector3(hAxis, 0, vAxis) * moveSpeedTemp * Time.fixedDeltaTime;
        Vector3 newPos = rb.position + rb.transform.TransformDirection(movement);
        rb.MovePosition(newPos);

        moveSpeedTemp = movementSpeed;
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_grounded()) { rb.AddForce(0, jumpForce, 0, ForceMode.Impulse); }
        }
    }

    private bool _grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, jumpRaycastDistance);
    }

    private bool _sprint()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }
}
