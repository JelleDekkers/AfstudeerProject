using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static Inventory Inventory;
    public static EquippedItemManager equippedItemManager;

    public static float HealthPoints { get; private set; }
    public static float AttackPoints { get; private set; }
    public static float ArmorPoints { get; private set; }
    public static float ShieldPoints { get; private set; }

    public static Player Instance;

    private void Start() {
        if (Instance == null)
            Instance = this;
        Inventory = new Inventory();
        equippedItemManager = GetComponent<EquippedItemManager>();
        Inventory.OnEquipmentChanged += UpdateStats;
    }

    private void UpdateStats() {
        print("updatestats");
        ArmorPoints = Inventory.GetTotalArmorPoints();
        AttackPoints = Inventory.GetWeaponAttackPoints();
        ShieldPoints = Inventory.GetShieldPoints();
    }
}
