using UnityEngine;
using System.Collections;

public abstract class Trigger : InteractableObject {
    public override abstract void InteractWith();
    protected abstract void OnInteractedWith();
}
