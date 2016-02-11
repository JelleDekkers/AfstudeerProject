using UnityEngine;
using System.Collections;

public class Shield : Item {

    public override void InteractWith() {
        OnPickedUp();
    }

    protected override void OnPickedUp() {
        //play sound
    }
}
