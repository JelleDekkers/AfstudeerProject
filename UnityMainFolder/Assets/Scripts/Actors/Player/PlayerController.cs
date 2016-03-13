using UnityEngine;
using System.Collections;

public class PlayerController : HumanoidController {

    private float xRotation = 0;
    private float yRotation = 0;
    private float verticalInput;
    private float horizontalInput;

    [SerializeField]
    private float rotationSpeed = 8;

    public override void Start() {
        base.Start();

        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;
    }

    public override void Update() {
        base.Update();

        if (PlayerState.State == playerState.InGame && Player.Instance.HealthPoints > 0) {
            // Look rotation:
            transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
            xRotation += Input.GetAxis("Mouse X") * rotationSpeed;
            yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;

            //if (Player.Inventory.GetWeapon != null) {
            // Attacking:
            if (Input.GetMouseButtonDown(PlayerInput.AttackButton)) {
                anim.SetBool("Attacking", true);
            }
            //Blocking:
            if (Input.GetMouseButtonDown(PlayerInput.BlockButton)) {
                anim.SetBool("Blocking", true);
                Player.Instance.Block();
            } else if (Input.GetMouseButtonUp(PlayerInput.BlockButton)) {
                anim.SetBool("Blocking", false);
                Player.Instance.StopBlocking();
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
}
