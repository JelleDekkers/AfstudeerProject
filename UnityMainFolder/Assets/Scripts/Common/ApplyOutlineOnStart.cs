using UnityEngine;
using System.Collections;

public class ApplyOutlineOnStart : MonoBehaviour {

	void Awake() {
        OutlineMaterialManager.ChangeMatsToItemMats(gameObject);
        Destroy(this);
	}
}
