﻿using UnityEngine;
using System.Collections;

public class EquippedItemHolder : MonoBehaviour {

	public void UpdateHolder(ItemData item) {
        // instantiate item
        if(transform.childCount > 0) {
            Destroy(transform.GetChild(0).gameObject);
        }
        if (item != null) {
            //instantiate new gameobject with item, setparent transform
        }
    }
}
