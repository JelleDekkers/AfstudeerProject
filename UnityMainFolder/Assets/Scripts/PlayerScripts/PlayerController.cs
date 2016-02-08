﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator anim;
    private bool pressingForward = false;
    private float speed = 0;
    private float xRotation = 0;
    private float yRotation = 0;
    private float verticalInput;
    private float horizontalInput;

    [SerializeField]
    private float rotationSpeed = 8;

    private void Start() {
        anim = GetComponent<Animator>();

        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;

        if (anim.layerCount == 2)
            anim.SetLayerWeight(1, 1);
    }

    private void Update() {

        if (PlayerState.State == playerState.InGame) {
            // Look rotation:
            transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
            xRotation += Input.GetAxis("Mouse X") * rotationSpeed;
            yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;

            //Attacking:
            if (Input.GetMouseButtonDown(0)) {
                anim.SetBool("Attacking", true);
            }
            if (Input.GetMouseButtonDown(1)) {
                anim.SetBool("Attacking", false);
            }
        }

        //Prevent strange rotations:
        if (yRotation > 180)
            yRotation -= 360;
        if (yRotation < -180)
            yRotation += 360;
        yRotation = Mathf.Clamp(yRotation, -60, 60);

        //Moving Forward:
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        anim.SetFloat("MovementZ", verticalInput);
        anim.SetFloat("MovementX", horizontalInput);
    }
}
