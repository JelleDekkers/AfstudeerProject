using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Inventory Inventory;
    public static EquippedItemManager equippedItemManager;

    public static float HealthPoints;
    public static float AttackPoints = 1;
    public static float ArmorPoints = 1;

    public int InventoryLength; //testing
    public ItemData[] items;


    private void Start() {
        Inventory = new Inventory();
        equippedItemManager = GetComponent<EquippedItemManager>();
    }

    private void Update() {
        items = Inventory.Items.ToArray();
        InventoryLength = Inventory.Items.Count;
    }
}
