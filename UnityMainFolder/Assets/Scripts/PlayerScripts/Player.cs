using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Inventory Inventory;
    public static EquippedItemManager equippedItemManager;

    public float attackPoints = 1;
    public float armorPoints = 1;

    public int InventoryLength; //testing

    private void Start() {
        Inventory = new Inventory();
        equippedItemManager = GetComponent<EquippedItemManager>();
    }

    private void Update() {
        InventoryLength = Inventory.Items.Count;
    }
}
