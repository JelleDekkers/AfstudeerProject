using UnityEngine;
using System.Collections;

public class AIController : HumanoidController {

    private NavMeshAgent navAgent;
    private float targetDistance;

    [SerializeField] private Transform target;

    public override void Start() {
        base.Start();
        actor = GetComponent<Actor>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public override void Update() {
        base.Update();

        if (actor.HealthPoints <= 0)
            return;

        TestInput();

        if (target != null) {
            navAgent.SetDestination(target.transform.position);
            targetDistance = navAgent.remainingDistance;

            if (targetDistance >= actor.Inventory.Weapon.WeaponLength) {
                MoveToTarget();
            } else {
                StopMoving();
                Attack();
            }
        }
    }

    private void MoveToTarget() {
        navAgent.Resume();
        float modifier = 2f;
        float z = anim.GetFloat("MovementZ");
        z = Mathf.MoveTowards(z, 1, modifier * Time.deltaTime);
        anim.SetFloat("MovementZ", z);
    }

    private void StopMoving() {
        float modifier = 2f;
        navAgent.Stop();
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
}
