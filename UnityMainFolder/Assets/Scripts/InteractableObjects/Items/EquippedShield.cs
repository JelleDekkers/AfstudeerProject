using UnityEngine;
using System.Collections;

public class EquippedShield : MonoBehaviour {

    private Actor wielder;
    private Collider collider;

    public void Init(Actor actor) {
        wielder = actor;
    }

    public void Start() {
        collider = GetComponent<CapsuleCollider>();
        collider.enabled = false;
    }

    public void Block() {
        collider.enabled = true;
    }

    public void UnBlock() {
        collider.enabled = false;
    }

    //private void OnTriggerEnter(Collider col) {
    //    if (col.GetComponent<Actor>())
    //        actor.AttackActor(col.GetComponent<Actor>());
    //    else
    //        Debug.Log("Col " + col.name + " has enemy layer but no actor component!");
    //}
}
