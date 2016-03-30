using UnityEngine;
using System.Collections;

public class Archer : Humanoid {

    [SerializeField] private Arrow arrowPrefab;
    [SerializeField] private Transform arrowStartPos;
    [SerializeField] private Transform shootDistance;
    [SerializeField] private Transform safeDistance;

    private Arrow arrowDrawn;
    private float waitTimeMin = 0.4f;
    private float waitTimeMax = 1.3f;
    private float waitTimer;

    protected override void Start() {
        base.Start();
        OnStaggered += DrawnArrowInterupt;
    }

    public override void Update() {
        base.Update();

        if(arrowDrawn != null) {
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
                InCombat();
                break;
            case State.InCombat:
                InCombat();
                break;
        }
    }

    private void Aggroed() {
        //walk to shoot position
    }

    private void InCombat() {
        DrawArrow();
        transform.LookAt(targetActor.transform);
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
        arrowDrawn.Init(transform);
        Quaternion newRotation = Quaternion.LookRotation(GetComponent<Actor>().attackCenter.transform.TransformDirection(Vector3.forward));
        arrowDrawn.transform.rotation = newRotation;
        waitTimer = Random.Range(waitTimeMin, waitTimeMax);
    }

    private void DrawnArrowInterupt() {
        if(arrowDrawn != null)
            Destroy(arrowDrawn.gameObject);
    }
}
