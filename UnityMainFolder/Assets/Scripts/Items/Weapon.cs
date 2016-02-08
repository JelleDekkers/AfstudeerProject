using UnityEngine;
using System.Collections;
using System;

public class Weapon : Item {

    [SerializeField] private float attackPoints = 1;
    public float AttackPoints { get { return attackPoints; } }

    public override void InteractWith() {
        print("interacting");
        OnPickedUp();
    }

    protected override void OnPickedUp() {
        //play sound
    }
}
