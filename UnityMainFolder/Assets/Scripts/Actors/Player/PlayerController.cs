using UnityEngine;
using System.Collections;

public class PlayerController : HumanoidAnimatorHandler {

    private float xRotation = 0;
    private float yRotation = 0;
    private Rigidbody rBody;
   
    [SerializeField] private float groundCheckRayLength = 0.4f;
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

        if (PlayerState.State == playerState.InGame && Player.Instance.CurrentHealthPoints > 0) {
            // Look rotation:
            if (Time.timeScale > 0) {
                transform.rotation = Quaternion.Euler(yRotation, xRotation, 0);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
                xRotation += Input.GetAxis("Mouse X") * rotationSpeed;
                yRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;
            }

            //if (Player.Inventory.GetWeapon != null) {
            // Attacking:
            if (Input.GetMouseButtonDown(PlayerInput.AttackButton)) 
                Attack();
            
            // Block:
            if (Input.GetMouseButtonDown(PlayerInput.BlockButton)) 
                Block();
            else if (Input.GetMouseButtonUp(PlayerInput.BlockButton)) 
                StopBlocking();
            
            // Jump:
            if(Input.GetKeyDown(PlayerInput.JumpButton)) 
                Jump();

            // Roll:
            if (Input.GetKeyDown(PlayerInput.RollButton) && (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0))
                Roll();

            // Check if grounded:
            GroundCheck();

            // While jumping:
            if(!IsGrounded) 
                InAirControl();

            // Prevent strange rotations:
            if (yRotation > 180)
                yRotation -= 360;
            if (yRotation < -180)
                yRotation += 360;
            yRotation = Mathf.Clamp(yRotation, -60, 60);

            //Movement:
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");
            anim.SetFloat("MovementZ", verticalInput);
            anim.SetFloat("MovementX", horizontalInput);
        }
    }

    //private void Move() {
    //    float verticalInput = Input.GetAxis("Vertical");
    //    float horizontalInput = Input.GetAxis("Horizontal");
       
    //    if (verticalInput > 0)
    //        verticalVelocityMultiplier = forwardVelocityMultiplier;
    //    else
    //        verticalVelocityMultiplier = backVelocityMultiplier;

    //    // Normalized to prevent diagonal speed being too high
    //    Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
    //    movementDirection.x *= horizontalVelocityMultiplier;
    //    movementDirection.z *= verticalVelocityMultiplier;
    //    movementDirection = transform.TransformDirection(movementDirection);
    //    rBody.velocity = movementDirection;

    //    anim.SetFloat("MovementZ", movementDirection.z);
    //    anim.SetFloat("MovementX", movementDirection.x);
    //}
    
    private void Jump() {
        if (IsGrounded) {
            anim.SetBool("Blocking", false);
            anim.SetTrigger("Jump");
            IsJumping = true;
            rBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //rBody.constraints = RigidbodyConstraints.FreezeRotation;
            rBody.AddForce(transform.TransformDirection(Vector3.forward) * jumpForce, ForceMode.Impulse);
        }
    }

    private void InAirControl() {
        Vector3 velocity = rBody.velocity;
        float h = inAirMovementSpeed * Input.GetAxis("Horizontal");
        float v = inAirMovementSpeed * Input.GetAxis("Vertical");
        Vector3 pos = new Vector3(h, 0, v);
        rBody.AddRelativeForce(pos * inAirMovementSpeed, ForceMode.Force);
    }

    private void GroundCheck() {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * groundCheckRayLength, Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckRayLength, groundCheckLayerMask)) {
            if (IsJumping == false)
                IsGrounded = true;
        } else
            IsGrounded = false;

        if (InAir())
            IsJumping = false;

        if(!IsRolling)
            anim.SetBool("Grounded", IsGrounded);
    }
}
