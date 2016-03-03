using UnityEngine;
using System.Collections;

public class AIController : HumanoidController {

    private void Start() {
        InitController();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            anim.SetBool("Attacking", true);
        }

        if (rightArmLayerState.fullPathHash == UpperBodyLayer_LeftSwingState) { 
            anim.SetBool("Attacking", false); // set to false again to prevent loop.
        }
    }
}
