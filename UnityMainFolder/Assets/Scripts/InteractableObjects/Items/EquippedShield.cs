using UnityEngine;
using System.Collections;

public class EquippedShield : MonoBehaviour {

    private Actor wielder;
    private Collider collider;

    public void Init(Actor actor) {
        wielder = actor;
    }

    public void Start() {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    public void Block() {
        collider.enabled = true;
    }

    public void UnBlock() {
        collider.enabled = false;
    }
}
