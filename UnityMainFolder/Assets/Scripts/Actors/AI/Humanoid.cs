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
    protected bool firstHit;

    private bool targetInSight;
    private bool targetPosReached;

    protected virtual void Start() {
        navAgent = GetComponent<HumanoidNavHandler>();
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

        DetectEnemiesWithSphereCast();

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

    private void DetectEnemiesWithSphereCast() {
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

        if(targetIsNearby) {
            direction = target.transform.position - transform.position;
            angle = Vector3.Angle(direction, transform.forward);

            if (Physics.Raycast(attackCenter.transform.position, target.transform.position - transform.position, out hit, viewDistance, sightLayerMask)) {
                if (hit.collider.gameObject == target.gameObject && angle < fovAngle * 0.5f) {
                    targetInSight = true;
                    lastEnemySighting = hit.transform.position;

                    if (Vector3.Distance(transform.position, target.transform.position) <= attackDistance)
                        CurrentState = State.InCombat;
                    else
                        CurrentState = State.Aggroed;

                    return;
                }
            }
        }

        targetInSight = false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCenter.position, viewDistance);
    }

    //private void OnTriggerStay(Collider col) {
    //    if (col.gameObject != target.gameObject)
    //        return;

    //    Vector3 direction = col.transform.position - transform.position;
    //    float angle = Vector3.Angle(direction, transform.forward);

    //    if (angle < fovAngle * 0.5f) {
    //        RaycastHit hit;
    //        if (Physics.Raycast(attackCenter.transform.position, direction.normalized, out hit, sightLayerMask)) {
    //            if (hit.collider.gameObject == target.gameObject) {
    //                targetInSight = true;
    //                lastEnemySighting = target.transform.position;

    //                if (Vector3.Distance(transform.position, target.transform.position) <= attackDistance)
    //                    CurrentState = State.InCombat;
    //                else 
    //                    CurrentState = State.Aggroed;
    //            }
    //        }
    //    }
    //}

    protected virtual void Idle() {
        firstHit = true;
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
        firstHit = true;
        float facingAngle = 10;
        transform.SlowLookat(lastEnemySighting, rotationSpeed);
        navAgent.MoveToTargetPosition(lastEnemySighting);
        navAgent.OnTargetReachedEvent = delegate () {
            Vector3 direction = target.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);
            if (angle < facingAngle) {
                CurrentState = State.Idle;
                return;
            }
        };
    }

    protected virtual void InCombat() {
        navAgent.StopMoving();

        if(!targetInSight) 
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
}
