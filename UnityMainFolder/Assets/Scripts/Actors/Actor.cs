using UnityEngine;
using System.Collections;

public enum ActorState {
    ///<summary>Idle, just standing around</summary>
    Idle = 0,
    ///<summary>Alerted to an enemy presence and goes in to attack </summary> 
    Aggroed = 1,
    ///<summary>patrolling after being aggroed but having lost its target</summary> 
    Patrolling = 2,
    ///<summary>Attacking an enemy </summary> 
    InCombat = 3,
    ///<summary>Dead </summary> 
    Dead = 4
}

public class Actor : MonoBehaviour { //met interfaces IKillable, IDamageable???? 

    //public static float HealthPoints { get; private set; }
    //public static float AttackPoints { get; private set; }
    //public static float ArmorPoints { get; private set; }
    //public static float ShieldPoints { get; private set; }

    //protected int currentAlertLevel = 0;
    //protected float speed = 1;
    //protected float fovAngle;

    //private Inventory inventory = new Inventory();

    public virtual void AttackActor(Actor actor) {
        actor.TakeDamage(7f, this);
    }

    public virtual void TakeDamage(float amount, Actor sender) {
        print("Taking damage: " + amount + " from " + sender);
    }
}
