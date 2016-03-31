using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Humanoid : Actor {

    [SerializeField] private float viewDistance = 20;
    [SerializeField] private float fovAngle = 110f;
    [SerializeField] private LayerMask sightLayerMask;
    [SerializeField] private float animSpeed = 0.95f;

    protected HumanoidNavHandler navAgent;
    protected HumanoidAnimatorHandler animHandler;
    protected Vector3 lastDetectedEnemyPosition;
    protected GameObject[] detectedActors;
    protected Actor targetActor;
    private float patrollingToIdleTimerMax = 5;
    private bool targetReached;
    private Vector3 rndPoint;
    protected float attackDistance = 1;

    protected virtual void Start() {
        navAgent = GetComponent<HumanoidNavHandler>();
        animHandler = GetComponent<HumanoidAnimatorHandler>();
        anim.speed = animSpeed;
        OnDamageTaken += OnDamageTakenFunction;
    }

    public override void Update() {
        base.Update();
        detectedActors = GetDetectedActors();
        Debug.DrawRay(attackCenter.transform.position, transform.TransformDirection(Vector3.forward) * attackDistance, Color.red);
    }

    protected virtual GameObject[] GetDetectedActors() {
        List<GameObject> actorsInSight = new List<GameObject>();
        Collider[] nearbyItemColliders = Physics.OverlapSphere(attackCenter.position, viewDistance, sightLayerMask);
        RaycastHit hit;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 directionToTarget = player.transform.position - transform.position;
        if (Physics.Raycast(attackCenter.transform.position, directionToTarget, out hit, viewDistance, sightLayerMask)) {
            if (hit.collider.tag != "Wall")
                actorsInSight.Add(player);
        }

        return actorsInSight.ToArray();
    }


    protected virtual void Roam() {
        if (detectedActors.Length > 0) {
            targetActor = detectedActors[0].GetComponent<Actor>();
            currentState = State.Aggroed;
            return;
        }

        navAgent.OnTargetReachedEvent = delegate () {
            print("target reached while roaming");
            navAgent.StopMoving();
            targetReached = true;
            return;
        };

        navAgent.MoveToTargetPosition(navAgent.StartPos);
    }

    protected virtual void Patrol() {
        navAgent.MoveToTargetPosition(lastDetectedEnemyPosition);

        //lastDetectedEnemyPosition = controller.GetRandomNavPos();
        navAgent.OnTargetReachedEvent = delegate () {
            navAgent.StopMoving();
            print("targer reached while patrolling");
            currentState = State.Roaming;
            return;
        };
    }

    protected virtual void Aggroed() {
        if (detectedActors.Length == 0) {
            currentState = State.Patrolling;
            targetActor = null;
            return;
        }

        if (Vector3.Distance(transform.position, targetActor.transform.position) <= attackDistance) {
            navAgent.StopMoving();
            currentState = State.InCombat;
            return;
        }

        lastDetectedEnemyPosition = targetActor.transform.position;
        navAgent.MoveToTargetPosition(lastDetectedEnemyPosition);
    }


    private void OnDamageTakenFunction(GameObject cause) {
        if (cause == null)
            return;

        if (currentState == State.Roaming || currentState == State.Patrolling) { 
            if (cause.GetComponent<Player>() != null) 
                targetActor = cause.GetComponent<Player>();
            else
                currentState = State.Patrolling;
        } 
    }
}
