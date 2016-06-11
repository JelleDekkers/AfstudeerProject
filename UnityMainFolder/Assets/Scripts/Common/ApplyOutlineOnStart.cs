using UnityEngine;
using System.Collections;

public class ApplyOutlineOnStart : MonoBehaviour {

	void Awake() {
        OutlineMaterialManager.SwitchToOutlineMat(gameObject);
        Destroy(this);
	}
}
