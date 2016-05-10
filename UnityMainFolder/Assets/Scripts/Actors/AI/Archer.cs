using UnityEngine;
using System.Collections;

public class Archer : Humanoid {

    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private Transform arrowStartPos;
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
    }

    public override void Update() {
        base.Update();
        Debug.DrawRay(attackCenter.transform.position, transform.TransformDirection(Vector3.forward) * safeDistance, Color.blue);
    }

    protected override void InCombat() {
        //base.InCombat();

        if (arrowDrawn != null) {
            waitTimer -= Time.deltaTime;
            if (waitTimer < 0)
                animHandler.FireArrow();
        }

        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToPlayer < safeDistance)
            isRetreating = true;

        if (isRetreating == false) {
            DrawArrow();
            transform.LookAtWithoutYAxis(target.transform);
        } else {
            InterruptDrawnArrow();
            RetreatToSafeDistance();
        }
    }

    private void RetreatToSafeDistance() {
        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToPlayer > (safeDistance + attackDistance) / 2) {
            isRetreating = false;
            retreatPosSet = false;
            return;
        } 

        navHandler.OnTargetReachedEvent = delegate () {
            print("target reached event");
            retreatPosSet = false;
        };

        if(retreatPosSet == false) {
            Vector3 retreatPos = navHandler.GetRandomNavPosInDirection(transform.TransformDirection(Vector3.back));
            navHandler.MoveToTargetPosition(retreatPos);
            //GameObject test = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //test.transform.position = retreatPos;
            //Destroy(test.GetComponent<Collider>());
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

    // Instantiate arrow in scene:
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
