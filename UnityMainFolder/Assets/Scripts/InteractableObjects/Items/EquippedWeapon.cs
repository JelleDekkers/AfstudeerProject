using UnityEngine;
using System.Collections;

public class EquippedWeapon : MonoBehaviour {

    private Actor actor;

    public void Init(Actor actor) {
        this.actor = actor;
    }

	private void OnTriggerEnter(Collider col) {
        if (col.GetComponent<Actor>())
            actor.AttackActor(col.GetComponent<Actor>());
        else
            Debug.Log("Col " + col.name + " has enemy layer but no actor component!");
    }
}
