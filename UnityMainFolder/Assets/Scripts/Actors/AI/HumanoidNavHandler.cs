using UnityEngine;
using System;
using System.Collections;

public class HumanoidNavHandler : MonoBehaviour {

    public Vector3 StartPos { get; private set; }
    public Action OnTargetReachedEvent;

    private NavMeshAgent navAgent;
    private Vector3 targetPos;
    private Actor actor;
    private Animator anim;
    private HumanoidAnimatorHandler animHandler;
    private float stoppingDinstance;

    [SerializeField] private float maxSpeed = 0.9f;

    private void Start() {
        actor = GetComponent<Actor>();
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        animHandler = GetComponent<HumanoidAnimatorHandler>();
        StartPos = transform.position;
        stoppingDinstance = 4;
        actor.OnDeath += StopMoving;
        actor.OnDeath += delegate () {
            navAgent.enabled = false;
        };

        //navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
    }

    private void Update() {
        if (actor.CurrentHealthPoints <= 0)
            return;

        //if (actor.CurrentState != State.Aggroed || actor.CurrentState != State.InCombat)
        //    navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
        //else
        //    navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        if(navAgent.enabled && navAgent.remainingDistance < stoppingDinstance && OnTargetReachedEvent != null) {
            StopMoving();
            OnTargetReachedEvent();
        }
    }

    public void MoveToTargetPosition(Vector3 targetPos) {
        if (navAgent.enabled == false)
            return;

        if (this.targetPos != targetPos) {
            this.targetPos = targetPos;
            navAgent.SetDestination(targetPos);
        }

        navAgent.Resume();
        float modifier = 2f;
        float z = anim.GetFloat("MovementZ");
        if (GetComponent<Soldier>())
            z = Mathf.MoveTowards(z, maxSpeed, modifier * Time.deltaTime);
        else
            z = maxSpeed;
        anim.SetFloat("MovementZ", z);
    }

    public void StopMoving() {
        if (navAgent.enabled == false)
            return;

        navAgent.Stop();

        float modifier = 4f;
        float z = anim.GetFloat("MovementZ");
        z = Mathf.MoveTowards(z, 0, modifier * Time.deltaTime);
        anim.SetFloat("MovementZ", z);
    }

    public Vector3 GetRandomNavPosInDirection(Vector3 direction) {
        NavMeshHit hit;
        float radius = 10;
        Quaternion randAng = Quaternion.Euler(0, UnityEngine.Random.Range(-45, 45), 0);
        randAng = transform.rotation * randAng;                                         
        Vector3 spawnPos = transform.position + randAng * direction * 15;
        NavMesh.SamplePosition(spawnPos, out hit, radius, 1);
        return hit.position;
    }

    private Vector3 minBoundsPoint;
    private Vector3 maxBoundsPoint;
    private float boundsSize = float.NegativeInfinity;
    private Vector3 GetRandomTargetPoint() {
        if (boundsSize < 0) {
            minBoundsPoint = Vector3.one * float.PositiveInfinity;
            maxBoundsPoint = -minBoundsPoint;
            var vertices = NavMesh.CalculateTriangulation().vertices;
            foreach (var point in vertices) {
                if (minBoundsPoint.x > point.x)
                    minBoundsPoint = new Vector3(point.x, minBoundsPoint.y, minBoundsPoint.z);
                if (minBoundsPoint.y > point.y)
                    minBoundsPoint = new Vector3(minBoundsPoint.x, point.y, minBoundsPoint.z);
                if (minBoundsPoint.z > point.z)
                    minBoundsPoint = new Vector3(minBoundsPoint.x, minBoundsPoint.y, point.z);
                if (maxBoundsPoint.x < point.x)
                    maxBoundsPoint = new Vector3(point.x, maxBoundsPoint.y, maxBoundsPoint.z);
                if (maxBoundsPoint.y < point.y)
                    maxBoundsPoint = new Vector3(maxBoundsPoint.x, point.y, maxBoundsPoint.z);
                if (maxBoundsPoint.z < point.z)
                    maxBoundsPoint = new Vector3(maxBoundsPoint.x, maxBoundsPoint.y, point.z);
            }
            boundsSize = Vector3.Distance(minBoundsPoint, maxBoundsPoint);
        }
        var randomPoint = new Vector3(
            UnityEngine.Random.Range(minBoundsPoint.x, maxBoundsPoint.x),
            UnityEngine.Random.Range(minBoundsPoint.y, maxBoundsPoint.y),
            UnityEngine.Random.Range(minBoundsPoint.z, maxBoundsPoint.z)
        );
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, boundsSize, 1);
        return hit.position;
    }

    private void OnDestroy() {
        OnTargetReachedEvent = null;
    }
}
