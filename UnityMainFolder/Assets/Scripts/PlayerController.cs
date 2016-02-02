using UnityEngine;
using System.Collections;

public enum MovementState {
    Idle = 0,
    WalkingForward = 1,
    WalkingBack = 2,
}

[RequireComponent (typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private float forwardSpeed = 8;
    [SerializeField] private float backSpeed = 5;
    [SerializeField] private float sideSpeed = 6;
    [SerializeField] private float rotationSpeed = 8;
    [SerializeField] private bool hideCursor;

    private Rigidbody rigidBody;
    private float xRotation = 0;
    private float yRotation = 0;
    private float gravity = 0;
    private Vector3 movement;
    private float horizontalInput;
    private float verticalInput;

    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        rigidBody.useGravity = false;

        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;
    }

    void Update() {
        if (hideCursor)
            Cursor.visible = false;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(horizontalInput, 0, verticalInput);
        if (input.magnitude > 1) 
            input.Normalize();

        gravity = Physics.gravity.y * 3;
        float forwardMovement = forwardSpeed * input.z;
        if (verticalInput < 0)
            forwardMovement = backSpeed * input.z;
        movement = new Vector3(sideSpeed * input.x, 0, forwardMovement);
        movement = transform.TransformDirection(movement);
        movement.y = gravity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
        rigidBody.velocity = movement;

        xRotation += Input.GetAxis("Mouse X") * rotationSpeed;
        yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Prevent strange rotations:
        if (yRotation > 180)
            yRotation -= 360;
        if (yRotation < -180)
            yRotation += 360;
        yRotation = Mathf.Clamp(yRotation, -60, 60);

        // placeholder voor playerCamera
        //Camera.main.transform.rotation = transform.rotation;
        //Camera.main.transform.position = Camera.main.transform.rotation * new Vector3(0, 0, -3) + transform.position;
    }
}