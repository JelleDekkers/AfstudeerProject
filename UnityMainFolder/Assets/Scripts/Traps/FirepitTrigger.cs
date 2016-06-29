using UnityEngine;
using System.Collections;

public class FirepitTrigger : MonoBehaviour {

    private Firepit firePit;
    private GameObject fireFX;

	private void Start() {
        firePit = transform.parent.GetComponent<Firepit>();
        fireFX = transform.parent.GetChild(0).gameObject;
	}

	private void OnTriggerEnter(Collider col) {
        if(col.gameObject.tag == "Floor") {
            RaycastHit hit;
            Vector3 exitPoint = transform.parent.FindChild("Exit").transform.position;
            if (Physics.Raycast(exitPoint, Vector3.down, out hit)) {
                firePit.StartCoroutine(firePit.DistributeFire(hit.point));
            }
            //fireParticle.GetComponent<Fire>().Extinquish();
            fireFX.gameObject.SetActive(false);
        }
    }
}
