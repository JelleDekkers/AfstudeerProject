using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

    public static ParticleManager Instance;

    public GameObject Sparks;
    public GameObject Blood;

    private void Start() {
        if (Instance == null)
            Instance = this;
    }

    public static void InstantiateParticle(GameObject particle, Vector3 pos) {
        Instantiate(particle, pos, Quaternion.identity);
    }
}
