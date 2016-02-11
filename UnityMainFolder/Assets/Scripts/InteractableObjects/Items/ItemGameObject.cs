using UnityEngine;
using System.Collections;
using System;

public enum ItemType {
    Helmet,
    Cuirass,
    LeftPauldron,
    RightPauldron,
    LeftGreave,
    RightGreave,
    Weapon,
    Shield
}

public class ItemGameObject : InteractableObject {
    protected virtual void OnPickedUp() { }

    public ItemType Type;
    public string MeshName;
    public Sprite Sprite;
    public float Points;

    private void Awake() {
        MeshName = gameObject.name;
    }
}
