using UnityEngine;
using System;
using System.Collections;

public class HumanoidNavHandler : MonoBehaviour {

    private NavMeshAgent navAgent;
    private Vector3 targetPos;

    public Vector3 StartPos { get; private set; }
    public Action OnTargetReachedEvent;

    private float stoppingDinstance;
    private Actor actor;
    private Animator anim;

    private void Start() {
        actor = GetComponent<Actor>();
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        StartPos = transform.position;
        stoppingDinstance = 1f;
        actor.OnDeath += StopMoving;
    }

    private void Update() {
        if (actor.CurrentHealthPoints <= 0)
            return;

        if (actor.CurrentState != State.Aggroed || actor.CurrentState != State.InCombat)
            navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
        else
            navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        if(navAgent.remainingDistance < stoppingDinstance && OnTargetReachedEvent != null) {
            StopMoving();
            OnTargetReachedEvent();
        }
    }

    public void MoveToTargetPosition(Vector3 targetPos) {
        if (this.targetPos != targetPos) {
            this.targetPos = targetPos;
            navAgent.SetDestination(targetPos);
        }

        navAgent.Resume();
        float modifier = 2f;
        float z = anim.GetFloat("MovementZ");
        if (GetComponent<Soldier>())
            z = Mathf.MoveTowards(z, 1, modifier * Time.deltaTime);
        else
            z = 1;
        anim.SetFloat("MovementZ", z);
    }

    public void StopMoving() {
        navAgent.Stop();

        float modifier = 2f;
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
}
