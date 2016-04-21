using UnityEngine;
using System.Collections.Generic;

public class Spikes : MonoBehaviour, ITrap {

    [SerializeField] private float playerDamage;
    [SerializeField] private float aiDamage;

    private Transform spikesParent;
    private List<GameObject> spikes = new List<GameObject>();
    private Vector3 spikesOriginalPos;
    private int spikesYOffset = 1;
    private bool triggered;
    private float spikesTimerMax = 3f;
    public float spikesTimer = 3f;

    private void Start() {
        spikesParent = transform.GetChild(0);
        foreach (Transform child in spikesParent.transform)
            spikes.Add(child.gameObject);
        spikesOriginalPos = spikesParent.transform.position;
    }

    private void Update() {
        if (!triggered)
            return;

        if(spikesTimer > 0) {
            spikesTimer -= Time.deltaTime;
        } else {
            spikesParent.transform.position = spikesOriginalPos;
            triggered = false;
        }

    }

    private void OnTriggerEnter(Collider col) {
        if (!col.GetComponent<Actor>())
            return;

        OnTriggered(col.GetComponent<Actor>());
    }

    public void OnTriggered(Actor actor) {
        // play sound
        
        if(!triggered) {
            spikesParent.transform.position = new Vector3(spikesParent.transform.position.x,
                                              spikesParent.transform.position.y + spikesYOffset,
                                              spikesParent.transform.position.z);
            triggered = true;
        }

        if (actor.GetType() == typeof(Player))
            actor.TakeDamage(gameObject, playerDamage);
        else
            actor.TakeDamage(gameObject, aiDamage);

        spikesTimer = spikesTimerMax;
        ParticleManager.InstantiateParticle(ParticleManager.Instance.Blood, actor.transform.position);
    }
}
