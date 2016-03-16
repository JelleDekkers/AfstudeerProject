using UnityEngine;
using System.Collections.Generic;

public class Humanoid : Actor {

    [SerializeField] private float viewDistance = 20;
    [SerializeField] private float fovAngle = 110f;
    [SerializeField] private LayerMask sightLayerMask;

    private AIController controller;
    private Vector3 lastDetectedEnemyPosition;
    private GameObject[] detectedActors;
    private Actor targetActor;
    private float patrollingToIdleTimerMax = 5;
    private float stateResetTimer = 0;

    private Vector3 rndPoint;

    private void Start() {
        controller = GetComponent<AIController>();
    }

    public override void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            rndPoint = controller.GetRandomNavPos(Vector3.forward);
        }

        base.Update();
        detectedActors = GetDetectedActors();

        //TODO: nette class structuur opzetten
        //stil staan wanneer target behaald
        //eventuele patrol mode implementeren tijdens roam state, zelfde manier als bij patrol state
        //combat implementeren, slaan en blocken

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
        }
    } 

    private void Roam() {
        controller.MoveToTargetPosition(controller.StartPos);
        
        if (detectedActors.Length > 0) {
            targetActor = detectedActors[0].GetComponent<Actor>();
            currentState = State.Aggroed;
        }
    }

    private void Patrol() {
        controller.MoveToTargetPosition(lastDetectedEnemyPosition);
        //when target reached:
        //lastDetectedEnemyPosition = controller.GetRandomNavPos();
        
        stateResetTimer -= Time.deltaTime;

        if (stateResetTimer <= 0) {
            currentState = State.Roaming;
            stateResetTimer = patrollingToIdleTimerMax;
        }
    }

    private void Aggroed() {
        if (detectedActors.Length == 0) {
            currentState = State.Patrolling;
            targetActor = null;
            return;
        }

        lastDetectedEnemyPosition = targetActor.transform.position;
        if (Vector3.Distance(transform.position, lastDetectedEnemyPosition) < 2) {
            controller.MoveToTargetPosition(targetActor.transform.position);
        } else {
            //attack, block 
        }
    }

    private GameObject[] GetDetectedActors() {
        List<GameObject> actorsInSight = new List<GameObject>();
        Collider[] nearbyItemColliders = Physics.OverlapSphere(attackCenter.position, viewDistance, sightLayerMask);
        RaycastHit hit;

        foreach (Collider col in nearbyItemColliders) {
            Vector3 directionToTarget = col.gameObject.transform.position - transform.position;
            if (Physics.Raycast(attackCenter.transform.position, directionToTarget, out hit, viewDistance, sightLayerMask)) {

                if (hit.collider.tag == "Wall")
                    continue;

                directionToTarget = directionToTarget.normalized;
                float dot = Vector3.Dot(directionToTarget, transform.forward);
                float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

                if (angle < fovAngle / 2) 
                    actorsInSight.Add(col.gameObject);
            } 
        }
        return actorsInSight.ToArray();
    }
}
