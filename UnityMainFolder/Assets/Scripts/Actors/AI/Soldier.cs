using UnityEngine;
using System.Collections;

public class Soldier : Humanoid {

    private bool combatRoutineStarted;
    private bool kiteRoutineStarted;
    private int prevAction = 0;

    protected override void Start() {
        base.Start();
        attackDistance = Inventory.Weapon.WeaponLength;
    }

    protected override void InCombat() {
        base.InCombat();
        if (!combatRoutineStarted)
            StartCoroutine(RandomCombat());
        transform.SlowLookat(target.transform, rotationSpeed);
    }

    private IEnumerator RandomCombat() {
        int combatActions = 3;
        int rndBehaviour = Random.Range(0, combatActions);
        float time = 0;

        if (firstHit && Random.Range(0, 2) == 1) {
            animHandler.Attack();
            rndBehaviour = 0;
            firstHit = false;
        } else {
            if (rndBehaviour == prevAction) {
                if (rndBehaviour == combatActions - 1)
                    rndBehaviour = 0;
                else
                    rndBehaviour++;
            }
        }

        prevAction = rndBehaviour;
        combatRoutineStarted = true;

        switch (rndBehaviour) {
            case 0:
                time = 1f;
                animHandler.Attack();
                yield return new WaitForSeconds(time);
                break;
            case 1:
                time = Random.Range(1f, 2f);
                if (Inventory.Shield != null) {
                    animHandler.Block();
                    yield return new WaitForSeconds(time);
                    animHandler.StopBlocking();
                } else {
                    yield return new WaitForSeconds(time / 2);
                }
                break;
            case 2:
                time = Random.Range(0.5f, 1.5f);
                yield return new WaitForSeconds(time);
                break;
        }

        combatRoutineStarted = false;
    }

    private void Flank() {
        Vector3 flankPos = new Vector3(transform.position.x + Random.Range(1, 3), transform.position.y, transform.position.z + Random.Range(1, 3));
        navHandler.MoveToTargetPosition(flankPos);
    }
}
