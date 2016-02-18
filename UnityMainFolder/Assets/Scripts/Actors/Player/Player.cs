using UnityEngine;
using System.Collections;

public class Player : Actor {

    public static Inventory Inventory;
    public static EquippedItemHolderManager equippedItemManager;

    public static float HealthPoints { get; private set; }
    public static float AttackPoints { get; private set; }
    public static float ArmorPoints { get; private set; }
    public static float ShieldPoints { get; private set; }
    public static Player Instance;

    private LayerMask layerMask;
    private float attackSphereRange = 2;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        Inventory = new Inventory();
        equippedItemManager = GetComponent<EquippedItemHolderManager>();
        Inventory.OnEquipmentChanged += UpdateStats;
        layerMask = 1 << 9;
    }

    private void UpdateStats() {
        ArmorPoints = Inventory.GetTotalArmorPoints();
        AttackPoints = Inventory.GetWeaponAttackPoints();
        ShieldPoints = Inventory.GetShieldPoints();
    }

    public void BeginAttack() {
        if (Inventory.GetWeapon == null) 
            return;
        GameObject weapon = GetComponent<EquippedItemHolderManager>().WeaponHolder.Item;
        weapon.GetComponent<Collider>().enabled = true;
    }

    public void EndAttack() {
        if (Inventory.GetWeapon == null)
            return;
        GameObject weapon = GetComponent<EquippedItemHolderManager>().WeaponHolder.Item;
        weapon.GetComponent<Collider>().enabled = false;
    }

    public void Attack(GameObject g) {
        Debug.Log("attack: " + g);
    }

    public void Block() {
        if (Inventory.GetShield == null)
            return;
        GameObject shield = GetComponent<EquippedItemHolderManager>().ShieldHolder.Item;
        shield.GetComponent<Collider>().enabled = true;
    }

    public void Unblock() {
        if (Inventory.GetShield == null)
            return;
        GameObject shield = GetComponent<EquippedItemHolderManager>().ShieldHolder.Item;
        shield.GetComponent<Collider>().enabled = false;
    }
}
