using UnityEngine;
using System.Collections;
using System;

public class Armor : Item {

    [SerializeField] private float protectionPoints = 1;
    public float ProtectionPoints { get { return protectionPoints; } }

    public override void InteractWith() {
        print("interacting");
        OnPickedUp();
    }

    protected override void OnPickedUp() {
        //play sound
    }
}
