using UnityEngine;
using System.Collections;

public enum MovementState {
    Idle = 0,
    WalkingForward = 1,
    WalkingBack = 2,
}

public class PlayerController : MonoBehaviour {
    // generic controls
    [SerializeField] private float speed = 8;

    private float xRotation = 0;
    private float yRotation = 0;

    void Start() {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<Rigidbody>().useGravity = false;

        Vector3 angles = Camera.main.transform.eulerAngles;
        xRotation = angles.y;
        yRotation = angles.x;
    }

    void Update() {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = transform.TransformDirection(input);
        if (input.magnitude > 1) 
            input.Normalize();
        float gravity = Physics.gravity.y * 3;
        Vector3 move = GetComponent<Rigidbody>().velocity;

        Vector3 p1 = transform.position + Vector3.down * 0.5f;

        move = input * speed;
        move.y = gravity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
        Vector3 euler = transform.eulerAngles;
        euler.x = 0;
        transform.eulerAngles = euler;

        GetComponent<Rigidbody>().velocity = move;

        xRotation += Input.GetAxis("Mouse X") * 5;
        yRotation -= Input.GetAxis("Mouse Y") * 5;

        // placeholder voor playerCamera
        Camera.main.transform.rotation = transform.rotation;
        Camera.main.transform.position = Camera.main.transform.rotation * new Vector3(0, 0, -3) + transform.position;
    }
}