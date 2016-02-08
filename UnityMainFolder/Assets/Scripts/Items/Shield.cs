using UnityEngine;
using System.Collections;

public class Shield : Item {

    [SerializeField] private float blockPoints = 1;
    public float BlockPoints { get { return blockPoints; } }

    public override void InteractWith() {
        print("interacting");
        OnPickedUp();
    }

    protected override void OnPickedUp() {
        //play sound
    }
}
