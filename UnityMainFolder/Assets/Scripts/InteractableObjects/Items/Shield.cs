using UnityEngine;
using System.Collections;

public class Shield : ItemGameObject {

    public override void Interact() {
        OnPickedUp();
    }

    protected override void OnPickedUp() {
        //play sound
    }
}
