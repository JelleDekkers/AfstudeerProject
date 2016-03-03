using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour { //met interfaces IKillable, IDamageable???? 

    public float HealthPoints { get; protected set; }
    public float AttackPoints { get; protected set; }
    public float ArmorPoints { get; protected set; }
    public float ShieldPoints { get; protected set; }

    public Inventory Inventory;
    public EquippedItemHolderManager equippedItemManager;

    private void Awake() {
        Inventory = new Inventory();
        equippedItemManager = GetComponent<EquippedItemHolderManager>();
        Inventory.OnEquipmentChanged += UpdateStats;
    }

    private void UpdateStats() {
        Debug.Log("updateSTats");
        ArmorPoints = Inventory.GetTotalArmorPoints();
        AttackPoints = Inventory.GetWeaponAttackPoints();
        ShieldPoints = Inventory.GetShieldPoints();
    }

    public virtual void AttackActor(Actor actorToAttack) {
        actorToAttack.TakeDamage(7f, this);
    }

    public virtual void TakeDamage(float amount, Actor sender) {
        print("Taking damage: " + amount + " from " + sender.name);
    }

    public virtual void BeginAttack() {
        if (Inventory.GetWeapon == null) {
            Debug.Log("Trying to attack with no weapon present");
            return;
        }
        GameObject weapon = GetComponent<EquippedItemHolderManager>().WeaponHolder.Item;
        weapon.GetComponent<Collider>().enabled = true;
    }

    public virtual void EndAttack() {
        if (Inventory.GetWeapon == null)
            return;
        GameObject weapon = GetComponent<EquippedItemHolderManager>().WeaponHolder.Item;
        weapon.GetComponent<Collider>().enabled = false;
    }

    public void Block() {
        if (Inventory.GetShield == null)
            return;
        GameObject shield = GetComponent<EquippedItemHolderManager>().ShieldHolder.Item;
        shield.GetComponent<EquippedShield>().Block();
    }

    public void Unblock() {
        if (Inventory.GetShield == null)
            return;
        GameObject shield = GetComponent<EquippedItemHolderManager>().ShieldHolder.Item;
        shield.GetComponent<EquippedShield>().UnBlock();
    }

    public void GetStaggered() {
        GameObject weapon = GetComponent<EquippedItemHolderManager>().WeaponHolder.Item;
        weapon.GetComponent<Collider>().enabled = false;
    }
}
