using UnityEngine;
using System.Collections;

public class FirepitTrigger : MonoBehaviour {

    private Firepit firePit;
    private GameObject fireParticle;

	private void Start() {
        firePit = transform.parent.GetComponent<Firepit>();
        fireParticle = transform.parent.GetChild(0).gameObject;
	}

	private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Floor") {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit)) {
                firePit.StartCoroutine(firePit.DistributeFire(hit.point));
            }
            fireParticle.GetComponent<Fire>().Extinquish();
            gameObject.SetActive(false);
        }
    }
}
