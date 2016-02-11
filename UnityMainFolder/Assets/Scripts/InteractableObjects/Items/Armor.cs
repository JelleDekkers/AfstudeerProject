using UnityEngine;
using System.Collections;
using System;

public class Armor : ItemGameObject {

    public override void Interact() {
        OnPickedUp();
    }

    protected override void OnPickedUp() {
        //play sound
    }
}
