using UnityEngine;
using System.Collections;
using System;

public class Armor : Item {

    public override void InteractWith() {
        OnPickedUp();
    }

    protected override void OnPickedUp() {
        //play sound
    }
}
