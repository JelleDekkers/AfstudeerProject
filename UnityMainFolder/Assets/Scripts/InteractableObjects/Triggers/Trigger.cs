using UnityEngine;
using System.Collections;

public abstract class Trigger : InteractableObject {
    public virtual void InteractWith() { }
    protected abstract void OnInteractedWith();
}
