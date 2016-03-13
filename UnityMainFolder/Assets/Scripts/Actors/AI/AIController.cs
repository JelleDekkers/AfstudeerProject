using UnityEngine;
using System.Collections;

public class AIController : HumanoidController {

    private Actor actor;

    public override void Start() {
        base.Start();
        actor = GetComponent<Actor>();
    }

    public override void Update() {
        base.Update();

        if (actor.HealthPoints <= 0)
            return;

        TestInput();

        if (anim.GetBool("Blocking") == true) {
            actor.Block();
        } else { 
            actor.StopBlocking();
        }
    }

    private void TestInput() {
        if (Input.GetKeyDown(KeyCode.C)) {
            anim.SetBool("Attacking", true);
        } else if (Input.GetKeyDown(KeyCode.V)) {
            anim.SetBool("Blocking", true);
            actor.IsBlocking = true;
        } else if (Input.GetKeyDown(KeyCode.B)) {
            anim.SetBool("Blocking", false);
            actor.IsBlocking = false;
        }
    }
}
