using UnityEngine;
using System.Collections;

public class PlayerController : HumanoidAnimatorHandler {

    private float xRotation = 0;
    private float yRotation = 0;
    private float verticalInput;
    private float horizontalInput;
    private Rigidbody rBody;
    private bool isGrounded = true;
    private bool isJumping;
    private RaycastHit hit;
    private float groundCheckRayLength = 0.4f;

    public bool enableJumping = true;

    [SerializeField] private float inAirMovementSpeed = 5;
    [SerializeField] private float rotationSpeed = 8;
    [SerializeField] private float jumpForce = 500;
    [SerializeField] private LayerMask groundCheckLayerMask;

    protected override void Start() {
        base.Start();
        xRotation = transform.eulerAngles.x;
        yRotation = transform.eulerAngles.y;
        rBody = GetComponent<Rigidbody>();
    }

    protected override void Update() {
        base.Update();

        if(Input.GetKey(KeyCode.L)) {
            anim.SetBool("Lunge", true);
        }

        if (PlayerState.State == playerState.InGame && Player.Instance.CurrentHealthPoints > 0) {
            // Look rotation:
            transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
            xRotation += Input.GetAxis("Mouse X") * rotationSpeed;
            yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;

            //if (Player.Inventory.GetWeapon != null) {
            // Attacking:
            if (Input.GetMouseButtonDown(PlayerInput.AttackButton)) {
                Attack();
            }

            // Block:
            if (Input.GetMouseButtonDown(PlayerInput.BlockButton)) {
                Block();
            } else if (Input.GetMouseButtonUp(PlayerInput.BlockButton)) {
                StopBlocking();
            }

            // Jump:
            if(Input.GetKeyDown(PlayerInput.JumpButton)) {
                Jump();
            }

            // Check if grounded:
            GroundCheck();

            // While jumping:
            if(isJumping) {
                InAirControl();
            }

            // Prevent strange rotations:
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

    private void Jump() {
        if (isGrounded) {
            anim.SetTrigger("Jump");
            isJumping = true;
            rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rBody.constraints = RigidbodyConstraints.None;
            //rBody.AddForce(transform.TransformDirection(Vector3.forward) * jumpForce, ForceMode.Impulse);
        }
    }

    private void InAirControl() {
        float h = inAirMovementSpeed * Input.GetAxis("Horizontal");
        float v = inAirMovementSpeed * Input.GetAxis("Vertical");
        Vector3 pos = new Vector3(h, 0, v);
        rBody.AddRelativeForce(pos * inAirMovementSpeed, ForceMode.Force);
        //transform.Translate(v, h, 0);
    }

    private void GroundCheck() {
        if (!enableJumping) {
            isGrounded = true;
            return;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckRayLength, groundCheckLayerMask)) {
            if (isJumping == false) {
                isGrounded = true;
                rBody.constraints = RigidbodyConstraints.FreezePositionY;
            }
        } else {
            isGrounded = false;
            rBody.constraints = RigidbodyConstraints.None;
        }

        if (baseLayerState.fullPathHash == baseLayer_inAirState)
            isJumping = false;

        anim.SetBool("Grounded", isGrounded);

    }
}
