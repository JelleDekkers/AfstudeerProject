using UnityEngine;
using System.Collections;

public class PlayerControllerMecanim : MonoBehaviour {

    private Animator anim;

    private float speed = 0;
    private float xRotation = 0;
    private float yRotation = 0;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField]
    private float rotationSpeed = 8;

    private void Start() {
        anim = GetComponent<Animator>();

        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;
    }

    private void Update() {
        verticalInput = Input.GetAxis("Vertical");
        speed = verticalInput;          
        anim.SetFloat("Speed", speed);

        transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);

        xRotation += Input.GetAxis("Mouse X") * rotationSpeed;
        yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;
    }

    private void OnGUI() {
        GUI.Label(new Rect(10, 10, 1000, 20), "Speed: " + speed);
    }
}
