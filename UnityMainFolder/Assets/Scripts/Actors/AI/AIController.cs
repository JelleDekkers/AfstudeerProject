using UnityEngine;
using System;
using System.Collections;

public class AIController : HumanoidController {

    private NavMeshAgent navAgent;
    private Vector3 targetPos;

    public Vector3 StartPos { get; private set; }
    public Action OnTargetReachedEvent;

    private float stoppingDinstance;

    public override void Start() {
        base.Start();
        actor = GetComponent<Actor>();
        navAgent = GetComponent<NavMeshAgent>();
        StartPos = transform.position;
        stoppingDinstance = 3f;
    }

    public override void Update() {
        base.Update();

        if (actor.HealthPoints <= 0)
            return;

        if (actor.currentState != State.Aggroed || actor.currentState != State.InCombat)
            navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
        else
            navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        if(navAgent.remainingDistance < stoppingDinstance && OnTargetReachedEvent != null) {
            OnTargetReachedEvent();
        }
    }

    public void MoveToTargetPosition(Vector3 target) {
        if (targetPos != target) {
            targetPos = target;
            navAgent.SetDestination(target);
            navAgent.Resume();
        }

        float modifier = 2f;
        float z = anim.GetFloat("MovementZ");
        z = Mathf.MoveTowards(z, 1, modifier * Time.deltaTime);
        anim.SetFloat("MovementZ", z);
    }

    public void StopMoving() {
        navAgent.Stop();

        float modifier = 2f;
        float z = anim.GetFloat("MovementZ");
        z = Mathf.MoveTowards(z, 0, modifier * Time.deltaTime);
        anim.SetFloat("MovementZ", z);
    }

    private void TestInput() {
        if (Input.GetKeyDown(KeyCode.C)) {
            Attack();
        } else if (Input.GetKeyDown(KeyCode.V)) {
            Block();
        } else if (Input.GetKeyDown(KeyCode.B)) {
            StopBlocking();
        }
    }

    public void AttackWrapper() {
        Attack();
    }

    public void BlockWrapper() {
        Block();
    }

    public void StopBlockingWrapper() {
        StopBlocking();
    }

    public Vector3 GetRandomNavPos(Vector3 direction) {
        NavMeshHit hit;
        float radius = 10;
        Quaternion randAng = Quaternion.Euler(0, UnityEngine.Random.Range(-45, 45), 0);
        randAng = transform.rotation * randAng;                                         
        Vector3 spawnPos = transform.position + randAng * direction * 15;
        NavMesh.SamplePosition(spawnPos, out hit, radius, 1);
        return hit.position;
    }
}
