using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Humanoid : Actor {

    [SerializeField] private float viewDistance = 20;
    [SerializeField] private float fovAngle = 110f;
    [SerializeField] protected float attackDistance = 1;
    [SerializeField] private LayerMask sightLayerMask;
    [SerializeField] protected float rotationSpeed = 3;

    protected HumanoidNavHandler navAgent;
    protected HumanoidAnimatorHandler animHandler;
    protected Vector3 lastEnemySighting;
    protected Actor target;

    private List<GameObject> actorsInSight = new List<GameObject>();
    private SphereCollider hearCollider;
    private bool playerInSight;
    private bool targetPosReached;

    protected virtual void Start() {
        navAgent = GetComponent<HumanoidNavHandler>();
        animHandler = GetComponent<HumanoidAnimatorHandler>();
        OnDamageTaken += OnDamageTakenFunction;
        hearCollider = GetComponent<SphereCollider>();
        hearCollider.radius = viewDistance;
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

        switch (CurrentState) {
            case State.Idle:
                Idle();
                break;
            case State.Patrolling:
                Patrol();
                break;
            case State.Aggroed:
                Aggroed();
                break;
            case State.InCombat:
                InCombat();
                break;
        }
    }

    private void OnTriggerStay(Collider col) {
        if (col.gameObject != target.gameObject)
            return;

        Vector3 direction = col.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fovAngle * 0.5f) {
            RaycastHit hit;
            if (Physics.Raycast(attackCenter.transform.position, direction.normalized, out hit, sightLayerMask)) {
                if (hit.collider.gameObject == target.gameObject) {
                    playerInSight = true;
                    lastEnemySighting = target.transform.position;

                    if (Vector3.Distance(transform.position, target.transform.position) <= attackDistance)
                        CurrentState = State.InCombat;
                    else 
                        CurrentState = State.Aggroed;
                }
            }
        }
    }

    private void OnTriggerExit(Collider col) {
        if(col.gameObject == target.gameObject) {
            playerInSight = false;
            CurrentState = State.Aggroed;
        }
    }

    //protected virtual GameObject[] GetDetectedActors() {
    //    Collider[] nearbyItemColliders = Physics.OverlapSphere(attackCenter.position, viewDistance, sightLayerMask);
    //    RaycastHit hit;
    //    Vector3 directionToTarget = player.transform.position - transform.position;
    //    if (Physics.Raycast(attackCenter.transform.position, directionToTarget, out hit, viewDistance, sightLayerMask)) {
    //        if (hit.collider.tag != "Wall" && !actorsInSight.Contains(player.gameObject)) {
    //            actorsInSight.Add(player.gameObject);
    //        }
    //    }
    //    return actorsInSight.ToArray();
    //}

    protected virtual void Idle() {
        navAgent.OnTargetReachedEvent = delegate () {
            targetPosReached = true;
            navAgent.StopMoving();
            return;
        };

        if (!targetPosReached) {
            transform.SlowLookat(navAgent.StartPos, rotationSpeed);
            navAgent.MoveToTargetPosition(navAgent.StartPos);
        }
    }

    protected virtual void Patrol() {
        //navAgent.MoveToTargetPosition(lastEnemySighting);
        //move to waypoints
    }

    protected virtual void Aggroed() {
        transform.SlowLookat(lastEnemySighting, rotationSpeed);
        navAgent.MoveToTargetPosition(lastEnemySighting);
        navAgent.OnTargetReachedEvent = delegate () {
            CurrentState = State.Idle;
            return;
        };
    }

    protected virtual void InCombat() {
        navAgent.StopMoving();
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
}
