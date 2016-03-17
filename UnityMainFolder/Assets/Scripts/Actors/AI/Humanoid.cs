using UnityEngine;
using System.Collections;
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
    private float lungeDistance = 3f;
    private bool combatRoutineStarted;

    private void Start() {
        controller = GetComponent<AIController>();
        lungeDistance += 1;
    }

    public override void Update() {
        Debug.DrawRay(attackCenter.transform.position, transform.TransformDirection(Vector3.forward) * lungeDistance, Color.red);
        base.Update();
        detectedActors = GetDetectedActors();

        //TODO: nette class structuur opzetten
        //eventuele patrol mode implementeren tijdens roam state, zelfde manier als bij patrol state

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

    private void Roam() {
        if (detectedActors.Length > 0) {
            targetActor = detectedActors[0].GetComponent<Actor>();
            currentState = State.Aggroed;
            return;
        }

        controller.OnTargetReachedEvent = delegate () {
            controller.StopMoving();
            return;
        };

        //controller.MoveToTargetPosition(controller.StartPos);
    }

    private void Patrol() {
        controller.MoveToTargetPosition(lastDetectedEnemyPosition);
        //when target reached:
        //lastDetectedEnemyPosition = controller.GetRandomNavPos();
        controller.OnTargetReachedEvent = delegate () {
            controller.StopMoving();
            currentState = State.Roaming;
            return;
        };

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

        if (Vector3.Distance(transform.position, targetActor.transform.position) <= lungeDistance) {
            controller.StopMoving();
            currentState = State.InCombat;
            return;
        }

        lastDetectedEnemyPosition = targetActor.transform.position;
        controller.MoveToTargetPosition(lastDetectedEnemyPosition);
    }

    private void InCombat() {
        if (detectedActors.Length == 0) {
            currentState = State.Patrolling;
            targetActor = null;
            return;
        }

        if (Vector3.Distance(transform.position, targetActor.transform.position) < lungeDistance) {
            transform.LookAt(targetActor.transform);
            controller.StopMoving();
            if(!combatRoutineStarted)
                StartCoroutine(RandomCombat());

        } else {
            currentState = State.Aggroed;
        }
    }

    private IEnumerator RandomCombat() {
        int rndBehaviour = Random.Range(0, 2);
        float time = 0;

        combatRoutineStarted = true;
        switch (rndBehaviour) {
            case 0:
                controller.AttackWrapper();
                yield return new WaitForSeconds(1f);
                break;
            case 1:
                time = Random.Range(0.5f, 4f);
                controller.BlockWrapper();
                yield return new WaitForSeconds(1f);
                controller.StopBlockingWrapper();
                break;
            case 2:
                time = Random.Range(0.3f, 1.5f);
                yield return new WaitForSeconds(1f);
                break;
        }

        combatRoutineStarted = false;
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

                //directionToTarget = directionToTarget.normalized;
                //float dot = Vector3.Dot(directionToTarget, transform.forward);
                //float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

                //if (angle < fovAngle / 2) 
                    actorsInSight.Add(col.gameObject);
            } 
        }

        return actorsInSight.ToArray();
    }

    private void OnGUI() {
        GUI.Label(new Rect(10, 10, 1000, 20), "State: " + currentState.ToString());
    }
}
