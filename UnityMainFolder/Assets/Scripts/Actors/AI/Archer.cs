﻿using UnityEngine;
using System.Collections;

public class Archer : Humanoid {

    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private Transform arrowStartPos;
    [SerializeField] private float shootDistance = 11;
    [SerializeField] private float safeDistance = 8;

    private Arrow arrowDrawn;
    private float waitTimeMin = 0.4f;
    private float waitTimeMax = 1.3f;
    private float waitTimer;
    private bool isRetreating;
    private bool retreatPosSet;

    protected override void Start() {
        base.Start();
        OnStaggered += InterruptDrawnArrow;
        attackDistance = shootDistance;
    }

    public override void Update() {
        base.Update();

        Debug.DrawRay(attackCenter.transform.position, transform.TransformDirection(Vector3.forward) * safeDistance, Color.blue);

        if (arrowDrawn != null) {
            waitTimer -= Time.deltaTime;
            if (waitTimer < 0)
                animHandler.FireArrow();
        }

        switch (currentState) {
            case State.Roaming:
                Roam();
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

    private void InCombat() {
        float distanceToPlayer = Vector3.Distance(transform.position, targetActor.transform.position);
        if (distanceToPlayer < safeDistance)
            isRetreating = true;

        if (isRetreating == false) {
            DrawArrow();
            transform.LookAtWithoutYAxis(targetActor.transform);
        } else {
            InterruptDrawnArrow();
            RetreatToSafeDistance();
        }
    }

    private void RetreatToSafeDistance() {
        float distanceToPlayer = Vector3.Distance(transform.position, targetActor.transform.position);
        if (distanceToPlayer > (safeDistance + attackDistance) / 2) {
            isRetreating = false;
            retreatPosSet = false;
            print("distance reahced");
            return;
        } 

        navAgent.OnTargetReachedEvent = delegate () {
            print("target reached event");
            retreatPosSet = false;
        };

        if(retreatPosSet == false) {
            navAgent.MoveToTargetPosition(navAgent.GetRandomNavPosInDirection(transform.TransformDirection(Vector3.back)));
            retreatPosSet = true;
        }
    }

    public void ShootArrow() {
        if (arrowDrawn == null)
            return;

        arrowDrawn.Fire();
        arrowDrawn = null;
    }

    public void DrawArrow() {
        animHandler.DrawArrow();
    }

    public void ShowArrowOnActor() {
        arrowDrawn = Instantiate(arrowPrefab, arrowStartPos.position, Quaternion.identity) as Arrow;
        arrowDrawn.Init(transform, AttackPoints);
        Quaternion newRotation = Quaternion.LookRotation(GetComponent<Actor>().attackCenter.transform.TransformDirection(Vector3.forward));
        arrowDrawn.transform.rotation = newRotation;
        waitTimer = Random.Range(waitTimeMin, waitTimeMax);
    }

    private void InterruptDrawnArrow() {
        if(arrowDrawn != null)
            Destroy(arrowDrawn.gameObject);
        animHandler.StopFiringArrow();
    }
}
