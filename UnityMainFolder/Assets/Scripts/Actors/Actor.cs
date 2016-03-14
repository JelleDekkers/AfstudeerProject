using UnityEngine;
using System;
using System.Collections;

public class Actor : MonoBehaviour { 

    public float HealthPoints = 100;// { get; protected set; }
    public float AttackPoints;// { get; protected set; }
    public float ArmorPoints;// { get; protected set; }
    public float ShieldPoints;// { get; protected set; }

    public Inventory Inventory;
    public bool IsBlocking;
    public Action OnDeath;

    private EquippedItemHolderManager equippedItemManager;
    private Animator anim;
    private RagdollController ragdollController;
    private HumanoidController humanoidController;
    private float forceAmount = 40;
    private int bodyLayer = 19;

    [SerializeField] private LayerMask attackLayerMask;
    [SerializeField] private Transform attackCentre;
    [SerializeField] private LayerMask shieldLayerMask;

    private void Awake() {
        Inventory = new Inventory();
        equippedItemManager = GetComponent<EquippedItemHolderManager>();
        anim = GetComponent<Animator>();
        ragdollController = GetComponent<RagdollController>();
        humanoidController = GetComponent<HumanoidController>();
        anim.SetFloat("HealthPoints", HealthPoints);
    }

    public virtual void Update() {
        anim.SetFloat("HealthPoints", HealthPoints);
    }

    protected void UpdateStats() {
        ArmorPoints = Inventory.GetTotalArmorPoints();
        AttackPoints = Inventory.GetWeaponAttackPoints();
        ShieldPoints = Inventory.GetShieldPoints();
    }

    public virtual void ExecuteAttack() {
        ItemData weapon = Inventory.Weapon;
        Collider[] objectsInRange = Physics.OverlapSphere(attackCentre.position, weapon.WeaponLength, attackLayerMask);
        GameObject objectHit = null;

        foreach (Collider col in objectsInRange) {
            Vector3 targetDir = col.transform.position - transform.position;
            targetDir = targetDir.normalized;
            float dot = Vector3.Dot(targetDir, transform.forward);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (angle < weapon.AttackAngle) {
                objectHit = col.gameObject;
                angle = Vector3.Angle(targetDir, objectHit.transform.position);

                if (objectHit.GetComponent<Actor>()) {
                    AttackActor(objectHit.GetComponent<Actor>(), angle);
                } else {
                    AttackProp(objectHit, fwd, forceAmount);
                }
            }
        }
    }

    public virtual void AttackActor(Actor actorToAttack, float angle) {
        Vector3 particlePos = Vector3.zero;
        if (actorToAttack.IsBlocking && angle < actorToAttack.Inventory.Shield.AttackAngle) {
            particlePos = actorToAttack.equippedItemManager.ShieldHolder.Item.transform.position;
            Instantiate(ParticleManager.Instance.Sparks, particlePos, Quaternion.identity);
        } else {
            particlePos = new Vector3(actorToAttack.transform.position.x, equippedItemManager.WeaponHolder.Item.transform.position.x, actorToAttack.transform.position.z);
            Instantiate(ParticleManager.Instance.Blood, particlePos, Quaternion.identity);
            float weaponAttackPoints = Inventory.Weapon.Points;
            actorToAttack.TakeDamage(weaponAttackPoints, this);
        }
    }

    public virtual void TakeDamage(float amount, Actor sender) {
        anim.SetBool("Flinch", true); //Stagger, Flinch
        HealthPoints -= amount;

        //if (HealthPoints <= 0)
            Die(sender);
    }

    private void Die(Actor killer) {
        HealthPoints = 0;
        humanoidController.SetUpperBodyLayerWeight(0);
        Vector3 direction = Common.GetDirection(killer.transform.position, transform.position);
        Common.SetLayerRecursively(bodyLayer, transform);
        ragdollController.ActivateRagDoll(direction, forceAmount);
        equippedItemManager.DropAndApplyForceToEquippedWeapons(direction, forceAmount);

        if (OnDeath != null)
            OnDeath();
    }

    private void AttackProp(GameObject objectHit, Vector3 forceDirection, float forceAmount) {
        if (!objectHit.GetComponent<Rigidbody>())
            return;

        objectHit.GetComponent<Rigidbody>().AddForce(forceDirection * forceAmount, ForceMode.Impulse);

        if (objectHit.GetComponent<DestructableObject>()) 
            objectHit.GetComponent<DestructableObject>().Hit(forceDirection, forceAmount);
        
    }

    public void EnableShieldCollider() {
        if (Inventory.Shield == null)
            return;
        GameObject shield = GetComponent<EquippedItemHolderManager>().ShieldHolder.Item;
        shield.GetComponent<Collider>().enabled = true;
    }

    public void DisableShieldCollider() {
        if (Inventory.Shield == null)
            return;
        GameObject shield = GetComponent<EquippedItemHolderManager>().ShieldHolder.Item;
        shield.GetComponent<Collider>().enabled = false;
    }
}
