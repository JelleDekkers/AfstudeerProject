﻿using UnityEngine;
using System.Collections;

public class Soldier : Humanoid {

    private float lungeDistance = 3f;
    private bool combatRoutineStarted;
    private int prevAction = 0;

    protected override void Start() {
        base.Start();
        attackDistance = lungeDistance;
    }

    public override void Update() {
        base.Update();

        if (currentState == State.Dead)
            return;

        // betere manier verzinnen voor wanneer moven en stoppen, boolean
        //doorzoeken naar lastdetected position en verder?
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

    private IEnumerator RandomCombat() {
        int combatActions = 4;
        int rndBehaviour = Random.Range(0, combatActions);
        float time = 0;

        if(rndBehaviour == prevAction) {
            if (rndBehaviour == combatActions - 1)
                rndBehaviour = 0;
            else
                rndBehaviour++;
        }
        prevAction = rndBehaviour;
        combatRoutineStarted = true;

        switch (rndBehaviour) {
            case 0:
                animHandler.Attack();
                yield return new WaitForSeconds(1f);
                break;
            case 1:
                time = Random.Range(1f, 2f);
                animHandler.Block();
                transform.LookAtWithoutYAxis(targetActor.transform);
                yield return new WaitForSeconds(time);
                animHandler.StopBlocking();
                break;
            case 2:
                time = Random.Range(0.5f, 1.5f);
                transform.LookAtWithoutYAxis(targetActor.transform);
                yield return new WaitForSeconds(time);
                break;
            case 3:
                animHandler.LungeAttack();
                yield return new WaitForSeconds(2f);
                break;
        }

        combatRoutineStarted = false;
    }

    public void InCombat() {
        if (detectedActors.Length == 0) {
            currentState = State.Patrolling;
            targetActor = null;
            return;
        }

        if (Vector3.Distance(transform.position, targetActor.transform.position) < lungeDistance) {
            navAgent.StopMoving();
            if (!combatRoutineStarted)
                StartCoroutine(RandomCombat());

        } else {
            currentState = State.Aggroed;
        }
    }

}
