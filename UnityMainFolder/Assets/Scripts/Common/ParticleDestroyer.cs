using UnityEngine;
using System.Collections;

public class ParticleDestroyer : MonoBehaviour {

    private ParticleSystem particle;
 
    private void Start() {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if (particle.time >= particle.duration && particle.particleCount == 0) 
            Destroy(gameObject);
    }
}
