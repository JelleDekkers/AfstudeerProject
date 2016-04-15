using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

    [SerializeField] private Transform target;

    public float distance = 10.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    private float wantedRotationAngle;
    private float wantedHeight;
    private float currentRotationAngle;
    private float currentHeight;

    private float shakeStrength = 4f;
    private float shakeTimeMultiplier = 1f;
    private float shakeTime;
    private float shakeMinifier = 1;
    public bool isShaking;

    public static PlayerCamera Instance;

    private void Start() {
        if (Instance == null)
            Instance = this;

        wantedHeight = target.position.y + height;
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
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Always look at the target
        transform.LookAt(target);

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

        if(Input.GetKeyDown(KeyCode.P)) {
            Shake(0.2f, 5, 3);
        }
        
    }

    public void Shake(float time, float strength, float posMinifier) {
        isShaking = true;
        shakeStrength = strength;
        shakeTime += time;
        if (posMinifier != 0)
            shakeMinifier = posMinifier;
        else
            shakeMinifier = 1;
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

