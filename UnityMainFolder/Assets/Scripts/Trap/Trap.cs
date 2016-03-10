using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    [SerializeField]
    private float damagePoints = 1;

    private void OnCollisionEnter(Collision col) {
        print(col.gameObject.name);
    }
}
