using UnityEngine;
using System.Collections;

public class PlayerController : HumanoidController {

    private float xRotation = 0;
    private float yRotation = 0;
    private float verticalInput;
    private float horizontalInput;

    [SerializeField] private float rotationSpeed = 8;

    private void Start() {
        InitController();

        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;
    }

    private void Update() {
        if (PlayerState.State == playerState.InGame) {
            // Look rotation:
            transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
            xRotation += Input.GetAxis("Mouse X") * rotationSpeed;
            yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;

            baseLayerState = anim.GetCurrentAnimatorStateInfo(0);
            rightArmLayerState = anim.GetCurrentAnimatorStateInfo(1);

            if (rightArmLayerState.fullPathHash == UpperBodyLayer_LeftSwingState) {

            }

            //if (Player.Inventory.GetWeapon != null) {
            // Attacking:
            if (Input.GetMouseButtonDown(PlayerInput.AttackButton)) {
                anim.SetBool("Attacking", true);
            }
            if (rightArmLayerState.fullPathHash == UpperBodyLayer_LeftSwingState) { // if is attacking set to to false to prevent loop
                anim.SetBool("Attacking", false);
            }
            if(Input.GetMouseButtonDown(PlayerInput.BlockButton)) {
                anim.SetBool("Blocking", true);
                Player.Instance.Block();
            } else if(Input.GetMouseButtonUp(PlayerInput.BlockButton)) {
                anim.SetBool("Blocking", false);
                Player.Instance.Unblock();
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
