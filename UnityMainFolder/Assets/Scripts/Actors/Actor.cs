using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public enum State {
    ///<summary>Idle, just standing around</summary>
    Idle = 0,
    ///<summary>Alerted to an enemy presence and goes in to attack</summary> 
    Aggroed = 1,
    ///<summary>patrolling after being aggroed but having lost its target</summary> 
    Patrolling = 2,
    ///<summary>In Combat with an enemy</summary> 
    InCombat = 3,
    ///<summary>Dead</summary> 
    Dead = 4
}

public class Actor : MonoBehaviour {

    public State CurrentState {
        get { return currentState; }
        protected set {
            if (currentState != value) {
                currentState = value;
                if (onStateChange != null)
                    onStateChange();
            }
        }
    }
    public float CurrentHealthPoints; // { get; private set; }
    public float MaxHealthPoints = 100;// { get; protected set; }
    public float AttackPoints;// { get; protected set; }
    public float ArmorPoints;// { get; protected set; }
    public float ShieldPoints;// { get; protected set; }

    public Animator anim { get; private set; }

    public Inventory Inventory;
    public bool IsBlocking;
    public Action OnDeath;
    public Action<GameObject> OnDamageTaken;
    public Action OnStaggered;
    public uint Potions;

    [SerializeField] private State currentState = State.Idle;
    private EquippedItemHolderManager equippedItemManager;
    private RagdollController ragdollController;
    private HumanoidAnimatorHandler humanoidController;
    private float strikeForceAmount = 20;
    private float regenTarget;
    private bool isRegenerating;
    private bool isGrounded;

    protected Action onStateChange;

    [SerializeField] private LayerMask attackLayerMask;
    [SerializeField] private LayerMask shieldLayerMask;
    [SerializeField] public Transform attackCenter; //protected
    [SerializeField] public float regenSpeed = 1;

    private void Awake() {
        Inventory = new Inventory();
        equippedItemManager = GetComponent<EquippedItemHolderManager>();
        anim = GetComponent<Animator>();
        ragdollController = GetComponent<RagdollController>();
        humanoidController = GetComponent<HumanoidAnimatorHandler>();
        anim.SetFloat("HealthPoints", CurrentHealthPoints);
        CurrentState = State.Idle;
        CurrentHealthPoints = MaxHealthPoints;
    }

    public virtual void Update() {
        anim.SetFloat("HealthPoints", CurrentHealthPoints);

        if (CurrentState == State.Dead)
            return;

        if (CurrentHealthPoints <= 0)
            CurrentState = State.Dead;
    }

    protected void UpdateStats() {
        ArmorPoints = Inventory.GetTotalArmorPoints();
        AttackPoints = Inventory.GetWeaponAttackPoints();
        ShieldPoints = Inventory.GetShieldPoints();
    }

    public virtual void AttackAnimationEvent() {
        ItemData weapon = Inventory.Weapon;
        Collider[] objectsInRange = Physics.OverlapSphere(attackCenter.position, weapon.WeaponLength, attackLayerMask).Where(c => !c.isTrigger).ToArray();
        GameObject objectHit = null;

        foreach (Collider col in objectsInRange) {
            Vector3 targetDir = col.transform.position - transform.position;
            targetDir = targetDir.normalized;
            float dot = Vector3.Dot(targetDir, transform.forward);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            if (float.IsNaN(angle) ||  angle < weapon.AttackAngle) {
                objectHit = col.gameObject;

                angle = Mathf.Abs(Mathf.Abs(Mathf.DeltaAngle(transform.localEulerAngles.y, objectHit.transform.localEulerAngles.y)) - 180);

                if (objectHit.GetComponent<Actor>()) {
                    AttackActor(objectHit.GetComponent<Actor>(), angle);
                } else {
                    AttackProp(objectHit, fwd, strikeForceAmount);
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
            particlePos = new Vector3(actorToAttack.transform.position.x, equippedItemManager.WeaponHolder.Item.transform.position.y, actorToAttack.transform.position.z);
            Instantiate(ParticleManager.Instance.Blood, particlePos, Quaternion.identity);
            float weaponAttackPoints = Inventory.Weapon.Points;
            actorToAttack.TakeDamage(weaponAttackPoints, gameObject);
        }
    }

    public void TakeDamage(float amount, GameObject sender) {
        anim.SetBool("Flinch", true); //Stagger, Flinch
        if (OnStaggered != null)
            OnStaggered();

        CurrentHealthPoints -= amount;
        if (isRegenerating)
            regenTarget -= amount;

        if (CurrentHealthPoints <= 0)
            Die(sender);
        else if (OnDamageTaken != null)
            OnDamageTaken(sender);
    }

    private void Die(GameObject killer) {
        CurrentHealthPoints = 0;
        humanoidController.SetUpperBodyLayerWeight(0);
        Vector3 direction = Common.GetDirection(killer.transform.position, transform.position);
        Common.SetLayerRecursively(Layers.BODY_LAYER, transform);
        ragdollController.ActivateRagDoll(direction, strikeForceAmount);
        equippedItemManager.DropAndApplyForceToEquippedWeapons(direction, strikeForceAmount);
        CurrentState = State.Dead;

        if (OnDeath != null)
            OnDeath();
    }

    private void AttackProp(GameObject objectHit, Vector3 forceDirection, float forceAmount) {
        if (!objectHit.GetComponent<Rigidbody>())
            return;

        objectHit.GetComponent<Rigidbody>().AddForce(forceDirection * forceAmount, ForceMode.Impulse);

        IHittable hittableComponent = objectHit.GetComponent<IHittable>();
        if (hittableComponent != null)
           hittableComponent.Hit(this, forceDirection, forceAmount);
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

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Trap") {
            ITrap trap = col.GetComponent<ITrap>();
            trap.OnTriggered(GetComponent<Actor>());
        }
    }

    protected void UseHealthPotion() {
        int healthPotionStrength = 40;

        if (Potions <= 0)
            return;

        Potions--;

        if (CurrentHealthPoints + healthPotionStrength < MaxHealthPoints)
            regenTarget = CurrentHealthPoints + healthPotionStrength;
        else
            regenTarget = MaxHealthPoints;

        if (isRegenerating == false)
            StartCoroutine(RegainHealth());
    }

    private IEnumerator RegainHealth() {
        isRegenerating = true;
        while (CurrentHealthPoints < regenTarget) {
            CurrentHealthPoints += Time.deltaTime * regenSpeed;
            yield return new WaitForEndOfFrame();
        }
        CurrentHealthPoints = regenTarget;
        isRegenerating = false;
    }

    //public void SetOnFire() {
    //    if (fire == null) {
    //        GameObject f = ParticleManager.InstantiateParticle(ParticleManager.Instance.Fire, attackCenter.position);
    //        f.transform.SetParent(attackCenter, true);
    //        f.transform.localEulerAngles = new Vector3(-90, 0, 0);
    //        fire = f.GetComponent<Fire>();
    //    } else {
    //        //extend time
    //    }
    //}

    //private void TakeFireDamageOnActor() {
    //    if (fire == null)
    //        return;

    //    HealthPoints -= Time.deltaTime * fire.playerDamage / 2;
    //}
}