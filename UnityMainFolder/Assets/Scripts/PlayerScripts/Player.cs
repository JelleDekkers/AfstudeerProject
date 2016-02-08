using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Inventory Inventory;

    private void Start() {
        Inventory = new Inventory();
    }
}
