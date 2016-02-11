using UnityEngine;
using System.Collections;
using System;

public class Lever : Trigger {
    public override void InteractWith() {
        OnInteractedWith();
    }

    protected override void OnInteractedWith() {
        //play sound?
    }
}
