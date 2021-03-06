﻿using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private LayerMask wallAvoidanceLayerMask;

    public float currentDistance = 20.0f;
    public float wantedDistance = 20;
    public float heightDamping = 2.0f;
    public float minPeekDistance = 4.5f;
    public float peekSpeed = 3f;
    public bool isShaking;

    private float wantedRotationAngle;
    private float wantedHeight;
    private float currentRotationAngle;
    private float currentHeight;
    private float shakeStrength = 4f;
    private float shakeTimeMultiplier = 1f;
    private float shakeTime;
    private float shakeMinifier = 1;
    private RaycastHit hit;
    private Transform cameraRayCheck;

    public static PlayerCamera Instance;

    private void Start() {
        if (Instance == null)
            Instance = this;

        wantedHeight = transform.position.y;
        StartCoroutine(Wait(1));        
    }

    private IEnumerator Wait(float seconds) {
        yield return new WaitForEndOfFrame();
        cameraRayCheck = new GameObject().transform;
        cameraRayCheck.position = transform.position;
        cameraRayCheck.parent = Player.Instance.transform;
        cameraRayCheck.name = "Main Camera RayCast";
    }

    void LateUpdate() {
        if (!target) {
            Debug.LogWarning("No target set for PlayerCamera!");
            return;
        }

        // Calculate the current rotation angles
        wantedRotationAngle = target.eulerAngles.y;
        
        currentRotationAngle = transform.eulerAngles.y;
        currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentRotationAngle = wantedRotationAngle;

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * currentDistance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // If player is obstructed:
        if (cameraRayCheck != null && Physics.Raycast(cameraRayCheck.position, Player.Instance.transform.position - cameraRayCheck.position, out hit, 100000, wallAvoidanceLayerMask)) {
            currentDistance = Mathf.Lerp(currentDistance, minPeekDistance, Time.deltaTime * peekSpeed * 2);
        } else {
            currentDistance = Mathf.Lerp(currentDistance, wantedDistance, Time.deltaTime * peekSpeed);
        }

        transform.LookAt(target);
        //var targetRotation = Quaternion.LookRotation(targetObj.transform.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);


        // Shake:
        if (isShaking) {
            shakeTime -= Time.deltaTime;
            transform.position = new Vector3(transform.position.x + (SmoothRandom.GetVector3Ridged(shakeStrength, shakeTimeMultiplier).x / shakeMinifier),
                                            transform.position.y,
                                            transform.position.z + (SmoothRandom.GetVector3Ridged(shakeStrength, shakeTimeMultiplier).z / shakeMinifier));
            if(shakeTime < 0) {
                isShaking = false;
                shakeTime = 0;
            }
        }
    }

    public void Shake(float time, float strength, float posMinifier) {
        shakeStrength = strength;
        if (isShaking)
            return;

        shakeTime += time;
        if (posMinifier != 0)
            shakeMinifier = posMinifier;
        else
            shakeMinifier = 1;

        isShaking = true;
    }

    private IEnumerator ShakeCoRoutine(float strength) {
        isShaking = true;
        while (shakeTime > 0) {
            transform.position = new Vector3(transform.position.x + (SmoothRandom.GetVector3Ridged(shakeStrength, shakeTimeMultiplier).x / 2),
                                       transform.position.y,
                                       transform.position.z + (SmoothRandom.GetVector3Ridged(shakeStrength, shakeTimeMultiplier).z / 2));
            print("shaking");
            yield return new WaitForEndOfFrame();
        }
        isShaking = false;
        shakeTime = 0;
    }

    /*
    // For looking at something, maybe at character when in inventory?
    void FixedUpdate ()
    {
        // if we hold Alt
        if(Input.GetButton("Fire2") && lookAtPos)
        {
            // lerp the camera position to the look at position, and lerp its forward direction to match 
            transform.position = Vector3.Lerp(transform.position, lookAtPos.position, Time.deltaTime * smooth);
            transform.forward = Vector3.Lerp(transform.forward, lookAtPos.forward, Time.deltaTime * smooth);
        }
        else
        {	
            // return the camera to standard position and direction
            transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.deltaTime * smooth);	
            transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.deltaTime * smooth);
        }
    }
    */
}

