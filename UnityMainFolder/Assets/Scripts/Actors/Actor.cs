using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour { 

    public float HealthPoints = 100;// { get; protected set; }
    public float AttackPoints;// { get; protected set; }
    public float ArmorPoints;// { get; protected set; }
    public float ShieldPoints;// { get; protected set; }

    public Inventory Inventory;
    public bool IsBlocking;

    private EquippedItemHolderManager equippedItemManager;
    private Animator anim;

    [SerializeField] private LayerMask attackLayerMask;
    [SerializeField] private Transform attackCentre;
    [SerializeField] private LayerMask shieldLayerMask;

    private void Awake() {
        Inventory = new Inventory();
        equippedItemManager = GetComponent<EquippedItemHolderManager>();
        anim = GetComponent<Animator>();
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

    public virtual void AttackActor(Actor actorToAttack) {
        actorToAttack.TakeDamage(7f, this);
    }

    public virtual void TakeDamage(float amount, Actor sender) {
        anim.SetBool("Flinch", true); //Stagger, Flinch
        //HealthPoints -= amount;

        if(HealthPoints <= 0) 
            anim.GetComponent<HumanoidController>().SetUpperBodyLayerWeight(0);
    }

    public virtual void ExecuteAttack() {
        ItemData weapon = Inventory.GetWeapon;
        Collider[] actorsInRange = Physics.OverlapSphere(attackCentre.position, weapon.WeaponLength, attackLayerMask);
        Actor actorHit = null;
        Vector3 particlePos = Vector3.zero;

        foreach(Collider col in actorsInRange) {
            Vector3 targetDir = col.transform.position - transform.position;
            targetDir = targetDir.normalized;
            float dot = Vector3.Dot(targetDir, transform.forward);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (angle < weapon.AttackAngle) {
                actorHit = col.GetComponent<Actor>();
                angle = Vector3.Angle(targetDir, actorHit.transform.position);
                if (actorHit.IsBlocking && angle < actorHit.Inventory.GetShield.AttackAngle) { 
                    particlePos = actorHit.equippedItemManager.ShieldHolder.Item.transform.position;
                    Instantiate(ParticleManager.Instance.Sparks, particlePos, Quaternion.identity);
                } else {
                    particlePos = new Vector3(col.transform.position.x, equippedItemManager.WeaponHolder.Item.transform.position.x, col.transform.position.z);
                    Instantiate(ParticleManager.Instance.Blood, particlePos, Quaternion.identity);
                    actorHit.TakeDamage(weapon.Points, this);
                }
            }
        }
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

    public void Stagger() {
        GameObject weapon = GetComponent<EquippedItemHolderManager>().WeaponHolder.Item;
        weapon.GetComponent<Collider>().enabled = false;
    }

    public void Flinch() {
        
    }
}
