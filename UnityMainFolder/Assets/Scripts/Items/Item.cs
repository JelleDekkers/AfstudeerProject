using UnityEngine;
using System.Collections;
using System;

public enum ArmorType {
    Helmet,
    Cuirass,
    LeftPauldron,
    RightPauldron,
    LeftBracers,
    RightBracers,
    LeftGreave,
    RightGreave
}

public abstract class Item : InteractableObject {
    public override abstract void InteractWith();
    protected abstract void OnPickedUp();
}
