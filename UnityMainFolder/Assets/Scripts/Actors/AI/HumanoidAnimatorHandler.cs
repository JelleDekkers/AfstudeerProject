using UnityEngine;
using System.Collections;

public class HumanoidAnimatorHandler : MonoBehaviour {

    private enum Direction {
        left,
        right,
        None
    }

    protected Animator anim;
    protected Actor actor;
    protected float animSpeed = 1;
    protected CapsuleCollider col;

    protected int baseLayerIndex = 0;
    protected int upperBodyLayerIndex = 1;

    protected int baseLayer_locoState = Animator.StringToHash("Base Layer.Locomotion");
    protected int baseLayer_staggerState = Animator.StringToHash("Base Layer.Stagger"); 
    protected int baseLayer_jumpState = Animator.StringToHash("Base Layer.Jumping.Jump");
    protected int baseLayer_inAirState = Animator.StringToHash("Base Layer.Jumping.InAir");
    protected int baseLayer_rollState = Animator.StringToHash("Base Layer.RollForward");
    protected int UpperBodyLayer_flinchState = Animator.StringToHash("UpperBodyLayer.Flinch");
    protected int UpperBodyLayer_nothingState = Animator.StringToHash("UpperBodyLayer.Nothing");
    protected int UpperBodyLayer_LeftSwingState = Animator.StringToHash("UpperBodyLayer.Swing Left");
    protected int UpperBodyLayer_rightSwingState = Animator.StringToHash("UpperBodyLayer.Swing Right");

    private bool attackAgain;
    private Direction currentAttackDirection;
    private Direction nextAttackDirection;
    private float colHeightOrigin;
    private Vector3 colCenterOrigin;

    public bool IsGrounded = true;
    public bool IsJumping;
    public bool IsRolling;
    public AnimatorStateInfo baseLayerState;
    public AnimatorStateInfo upperBodyLayerState;

    protected virtual void Start() {
        anim = GetComponent<Animator>();
        actor = GetComponent<Actor>();
        col = GetComponent<CapsuleCollider>();
        colHeightOrigin = col.height;
        colCenterOrigin = col.center;
        anim.SetLayerWeight(upperBodyLayerIndex, 1);
    }

    protected virtual void Update() {
        SetLayerStatesToAnimator();
        ResetOneTimeStates();
        HandleAttackLinks();

        if (baseLayerState.fullPathHash == baseLayer_rollState) {
            IsRolling = true;
            Rolling();
        } else {
            IsRolling = false;
            col.height = colHeightOrigin;
            col.center = colCenterOrigin;
        }
    }

    public void SetUpperBodyLayerWeight(int weight) {
        anim.SetLayerWeight(upperBodyLayerIndex, weight);
    }

    private void SetLayerStatesToAnimator() {
        baseLayerState = anim.GetCurrentAnimatorStateInfo(0);
        upperBodyLayerState = anim.GetCurrentAnimatorStateInfo(1);
    }

    private void ResetOneTimeStates() {
        //if (upperBodyLayerState.fullPathHash == UpperBodyLayer_LeftSwingState)
        //     anim.SetBool("Attacking", false);
        if (baseLayerState.fullPathHash == baseLayer_staggerState)
            anim.SetBool("Stagger", false);
        if (upperBodyLayerState.fullPathHash == UpperBodyLayer_flinchState) 
            anim.SetBool("Flinch", false);

        if (anim.GetBool("DrawArrow") == true) {
            anim.SetBool("DrawArrow", false);
            anim.SetBool("ShootArrow", false);
        }
    }

    private void HandleAttackLinks() {
        if (upperBodyLayerState.fullPathHash == UpperBodyLayer_LeftSwingState)
            currentAttackDirection = Direction.left;
        else if (upperBodyLayerState.fullPathHash == UpperBodyLayer_rightSwingState)
            currentAttackDirection = Direction.right;
        else
            currentAttackDirection = Direction.None;

        if (upperBodyLayerState.fullPathHash == UpperBodyLayer_LeftSwingState && nextAttackDirection == Direction.left)
            nextAttackDirection = Direction.None;
        else if (upperBodyLayerState.fullPathHash == UpperBodyLayer_rightSwingState && nextAttackDirection == Direction.right)
            nextAttackDirection = Direction.None;
        else if (nextAttackDirection == Direction.None)
            anim.SetBool("Attacking", false);
    }

    public void Attack() {
        anim.SetBool("Attacking", true);

        if (upperBodyLayerState.fullPathHash == UpperBodyLayer_LeftSwingState && currentAttackDirection == Direction.left)
            nextAttackDirection = Direction.right;
        else if (upperBodyLayerState.fullPathHash == UpperBodyLayer_rightSwingState && currentAttackDirection == Direction.right)
            nextAttackDirection = Direction.left;
        else
            nextAttackDirection = Direction.None;
    }

    public bool InAir() {
        if (baseLayerState.fullPathHash == baseLayer_inAirState)
            return true;
        else
            return false;
    }

    public void DrawArrow() {
        anim.SetBool("DrawArrow", true);
    }

    public void FireArrow() {
        anim.SetBool("ShootArrow", true);
    }

    public void StopFiringArrow() {
        anim.SetBool("ShootArrow", false);
    }

    public void Block() {
        anim.SetBool("Blocking", true);
        actor.EnableShieldCollider();
        actor.IsBlocking = true;
    }

    public void StopBlocking() {
        anim.SetBool("Blocking", false);
        actor.DisableShieldCollider();
        actor.IsBlocking = false;
    }

    protected void Roll() {
        if (IsGrounded) {
            anim.SetTrigger("Roll");
        }
    }

    private void Rolling() {
        col.height = anim.GetFloat("ColliderHeight");
        col.center = new Vector3(0, anim.GetFloat("ColliderPosY"), 0);
    }
}
