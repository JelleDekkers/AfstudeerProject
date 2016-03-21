using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour, ITrap {

    public float playerDamage { get; private set; }
    public float aiDamage { get; private set; }

    private void Start() {
        playerDamage = 10;
        aiDamage = 10;
    }

    public void OnTriggered(Actor actor) {
        if(actor.GetType() == typeof(Player)) {
            actor.TakeDamage(playerDamage * Time.deltaTime, gameObject);
        } else {
            actor.TakeDamage(aiDamage * Time.deltaTime, gameObject);
        }
    }

    //private void OnTriggerEnter(Collider col) {
    //    if (col.GetComponent<Actor>()) {
    //        col.GetComponent<Actor>().SetOnFire();
    //    }
    //}

    private void OnTriggerStay(Collider col) {
        if(col.GetComponent<Actor>()) {
            OnTriggered(col.GetComponent<Actor>());
        }
    }

    public void Extinquish() {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<ParticleSystem>().enableEmission = false; 
    }

    public void ExtinquishAfterDelay(float delay) {
        StartCoroutine(ExtinquishAfterDelayCoroutine(delay));
    }

    private IEnumerator ExtinquishAfterDelayCoroutine(float seconds) {
        yield return new WaitForSeconds(seconds);
        Extinquish();
    }
}
