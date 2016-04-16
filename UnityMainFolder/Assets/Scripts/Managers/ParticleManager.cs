using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour {

    public static ParticleManager Instance;

    public GameObject Sparks;
    public GameObject Blood;
    public GameObject Fire;

    private void Start() {
        if (Instance == null)
            Instance = this;
    }

    public static GameObject InstantiateParticle(GameObject particle, Vector3 pos) {
        return Instantiate(particle, pos, Quaternion.identity) as GameObject;
    }
}
