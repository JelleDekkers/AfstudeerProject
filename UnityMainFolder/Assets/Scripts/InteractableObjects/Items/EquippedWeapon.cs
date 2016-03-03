using UnityEngine;
using System.Collections;

public class EquippedWeapon : MonoBehaviour {

    private Actor wielder;
    private Collider collider;

    public void Init(Actor actor) {
        wielder = actor;
    }

    public void Start() {
        collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    private void OnTriggerEnter(Collider col) {
        if (col.GetComponent<Actor>())
            wielder.AttackActor(col.GetComponent<Actor>());
        else
            Debug.Log("Col " + col.name + " has enemy layer but no actor component!");
    }
}
