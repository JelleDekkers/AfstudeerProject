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

    void LateUpdate() {
        if (!target) {
            Debug.LogWarning("No target set for PlayerCamera!");
            return;
        }

        // Calculate the current rotation angles
        wantedRotationAngle = target.eulerAngles.y;
        wantedHeight = target.position.y + height;
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

