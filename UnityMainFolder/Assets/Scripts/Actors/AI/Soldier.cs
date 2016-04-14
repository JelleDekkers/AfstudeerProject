using UnityEngine;
using System.Collections;

public class Soldier : Humanoid {

    private bool combatRoutineStarted;
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
        int combatActions = 3; //with lunge = 4
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
                time = 1f;
                animHandler.Attack();
                yield return new WaitForSeconds(time);
                break;
            case 1:
                time = Random.Range(1f, 2f);
                animHandler.Block();
                //transform.LookAtWithoutYAxis(target.transform);
                //var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return new WaitForSeconds(time);
                animHandler.StopBlocking();
                break;
            case 2:
                time = Random.Range(0.5f, 1.5f);
                //transform.LookAtWithoutYAxis(target.transform);
                //targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                yield return new WaitForSeconds(time);
                break;
            case 3:
                time = 2f;
                animHandler.LungeAttack();
                yield return new WaitForSeconds(time);
                break;
        }

        combatRoutineStarted = false;
    }
}
