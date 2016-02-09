using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Inventory Inventory;
    public float attackPoints = 1;
    public float armorPoints = 1;

    private void Start() {
        Inventory = new Inventory();
    }
}
