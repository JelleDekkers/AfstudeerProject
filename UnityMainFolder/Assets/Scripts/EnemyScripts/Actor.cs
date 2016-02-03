using UnityEngine;
using System.Collections;

public enum CharacterState {
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

public class Actor : MonoBehaviour {

    public float HealthPoints { get; private set; }
    public GameObject Target { get; private set; }

    protected int currentAlertLevel = 0; 
    protected float speed = 1;
    protected float fovAngle;

}
