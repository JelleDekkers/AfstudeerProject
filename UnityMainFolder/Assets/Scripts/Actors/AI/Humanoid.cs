using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Humanoid : Actor {

    [SerializeField] private float viewDistance = 20;
    [SerializeField] private float hearDistance = 10;
    [SerializeField] private float fovAngle = 110f;
    [SerializeField] protected float attackDistance = 1;
    [SerializeField] private LayerMask sightLayerMask;
    [SerializeField] private LayerMask jumpableObjectsLayerMask;
    [SerializeField] protected float rotationSpeed = 3;
    [SerializeField] protected float jumpForce = 500;
    [SerializeField] private LayerMask groundCheckLayerMask;

    protected HumanoidNavHandler navHandler;
    protected HumanoidAnimatorHandler animHandler;
    protected Vector3 lastEnemySighting;
    protected Actor target;
    protected bool firstHit;

    private bool targetInSight;
    private bool targetPosReached;
    private bool jumped;

    protected virtual void Start() {
        navHandler = GetComponent<HumanoidNavHandler>();
        animHandler = GetComponent<HumanoidAnimatorHandler>();
        OnDamageTaken += OnDamageTakenFunction;
        target = Player.Instance;

        onStateChange = delegate () {
            targetPosReached = false;
            return;
        };
    }

    public override void Update() {
        anim.SetBool("Grounded", true);
        base.Update();
        if (CurrentState == State.Dead)
            return;

        Debug.DrawRay(attackCenter.transform.position, transform.TransformDirection(Vector3.forward) * attackDistance, Color.red);

        GroundCheck();
        DetectEnemies();
        InAirControl();

        switch (CurrentState) {
            case State.Idle:
                Idle();
                break;
            case State.Patrolling:
                Patrol();
                break;
            case State.Aggroed:
                Aggroed();
                //CheckForJumpableObstacles();
                break;
            case State.InCombat:
                InCombat();
                break;
        }
    }

    private void DetectEnemies() {
        RaycastHit hit;
        Vector3 direction;
        float angle;
        LayerMask playerLayerMask = 1 << Layers.PLAYER_LAYER;
        Collider[] hitColliders = Physics.OverlapSphere(attackCenter.position, viewDistance, playerLayerMask);
        bool targetIsNearby = false;

        foreach (Collider col in hitColliders) {
            if (col.gameObject == target.gameObject) {
                targetIsNearby = true;
            }
        }

        if (targetIsNearby) {
            direction = target.transform.position - transform.position;
            angle = Vector3.Angle(direction, transform.forward);

            if (Physics.Raycast(attackCenter.transform.position, target.transform.position - transform.position, out hit, viewDistance, sightLayerMask)) {
                if (hit.collider.gameObject == target.gameObject) {
                    // In sight of actor:
                    if (angle < fovAngle * 0.5f) {
                        targetInSight = true;
                        lastEnemySighting = hit.transform.position;

                        if (Vector3.Distance(transform.position, target.transform.position) <= attackDistance)
                            CurrentState = State.InCombat;
                        else
                            CurrentState = State.Aggroed;

                    // in hearing distance:
                    } else if (Vector3.Distance(transform.position, target.transform.position) <= hearDistance && animHandler.IsGrounded) {
                        transform.SlowLookat(target.transform, rotationSpeed);
                    }
                } else {
                    targetInSight = false;
                }
            }
        }
    }

    private void CheckForJumpableObstacles() {
        RaycastHit hit;
        float jumpDistance = 5;
        float jumpHeight = 3;

        // if something in front of feet:
        if (Physics.Raycast(transform.position, transform.forward, out hit, jumpDistance, jumpableObjectsLayerMask)) {

            // if something is not in front of jump height, it can be jumped over
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + jumpHeight, transform.position.z), transform.forward, out hit, jumpDistance, jumpableObjectsLayerMask) == false) {
                Jump();
                // and if it is a shortcut because this distance is shorter than path distance???
                // misschien checken of nav obstacle height vergelijken met jump height
                // random kans om er over heen te springen of om te lopen. dit voorkomt ook dat hij vast loopt?
            }
        }

        Debug.DrawRay(transform.position, transform.forward * jumpDistance, Color.red);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + jumpHeight, transform.position.z), transform.forward * jumpDistance, Color.red);
    }

    private void Jump() {
        // jump:
        if (animHandler.IsGrounded && animHandler.IsJumping == false) {
            navHandler.StopMoving();
            animHandler.IsJumping = true;
            jumped = true;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void InAirControl() {
        float inAirMovementSpeed = 10;
        Rigidbody rBody = GetComponent<Rigidbody>();

        if (animHandler.IsGrounded == false && animHandler.IsJumping) {
            rBody.AddRelativeForce(transform.forward * inAirMovementSpeed, ForceMode.Force);
        }
    }

    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(attackCenter.position, viewDistance);
    //}

    protected virtual void Idle() {
        firstHit = true;
        navHandler.OnTargetReachedEvent = delegate () {
            targetPosReached = true;
            navHandler.StopMoving();
            return;
        };

        if (!targetPosReached) {
            transform.SlowLookat(navHandler.StartPos, rotationSpeed);
            navHandler.MoveToTargetPosition(navHandler.StartPos);
        }
    }

    protected virtual void Patrol() {
        //navAgent.MoveToTargetPosition(lastEnemySighting);
        //move to waypoints
    }

    protected virtual void Aggroed() {
        firstHit = true;
        float facingAngle = 10;
        if(animHandler.IsGrounded)
            transform.SlowLookat(lastEnemySighting, rotationSpeed);
        navHandler.MoveToTargetPosition(lastEnemySighting);
        navHandler.OnTargetReachedEvent = delegate () {
            Vector3 direction = target.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < facingAngle) {
                CurrentState = State.Idle;
                return;
            }
        };
    }

    protected virtual void InCombat() {
        navHandler.StopMoving();

        if (!targetInSight)
            CurrentState = State.Aggroed;
    }

    private void OnDamageTakenFunction(GameObject cause) {
        if (cause == null)
            return;

        print("damage taken by: " + cause.name);

        if (cause.GetComponent<Player>() != null) {
            lastEnemySighting = cause.transform.position;
            CurrentState = State.Aggroed;
        }
    }

    private void GroundCheck() {
        RaycastHit hit;
        float groundCheckRayLength = 0.7f;

        Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        Debug.DrawRay(rayOrigin, Vector3.down * groundCheckRayLength, Color.red);
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, groundCheckRayLength, groundCheckLayerMask)) {
            if (animHandler.IsJumping == false)
                animHandler.IsGrounded = true;
        } else
            animHandler.IsGrounded = false;

        if (animHandler.InAir())
            animHandler.IsJumping = false;
        else if(animHandler.IsJumping == false && jumped)
            Land();

        if (!animHandler.IsRolling)
            anim.SetBool("Grounded", animHandler.IsGrounded);
    }

    private void Land() {
        GetComponent<NavMeshAgent>().enabled = true;

        if (target != null)
            navHandler.MoveToTargetPosition(target.transform.position);

        jumped = false;
    }
}
