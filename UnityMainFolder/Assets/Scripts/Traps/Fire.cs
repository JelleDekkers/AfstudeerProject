using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour, ITrap {

    [SerializeField] private float playerDamage;
    [SerializeField] private float aiDamage;

    public void OnTriggered(Actor actor) {
        if(actor.GetType() == typeof(Player)) {
            actor.TakeDamage(playerDamage * Time.deltaTime, gameObject);
        } else {
            actor.TakeDamage(aiDamage * Time.deltaTime, gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.GetComponent<Actor>()) {
            OnTriggered(other.GetComponent<Actor>());
        }
    }
}
