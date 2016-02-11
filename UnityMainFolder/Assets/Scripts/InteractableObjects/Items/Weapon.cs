using UnityEngine;
using System.Collections;
using System;

public class Weapon : ItemGameObject {

    public override void Interact() {
        OnPickedUp();
    }

    protected override void OnPickedUp() {
        //play sound
    }
}
