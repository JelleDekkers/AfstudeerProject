using UnityEngine;
using System.Collections;

public class HumanoidAnimatorHandler : MonoBehaviour {

    protected Animator anim;
    protected Actor actor;
    protected float animSpeed = 1;
    protected AnimatorStateInfo baseLayerState;
    protected AnimatorStateInfo upperBodyLayerState;

    protected int baseLayerIndex = 0;
    protected int upperBodyLayerIndex = 1;

    protected int baseLayer_locoState = Animator.StringToHash("Base Layer.Locomotion");
    protected int baseLayer_staggerState = Animator.StringToHash("Base Layer.Stagger"); 
    protected int baseLayer_lungeState = Animator.StringToHash("Base Layer.Lunge");
    protected int UpperBodyLayer_flinchState = Animator.StringToHash("UpperBodyLayer.Flinch");
    protected int UpperBodyLayer_nothingState = Animator.StringToHash("UpperBodyLayer.Nothing");
    protected int UpperBodyLayer_LeftSwingState = Animator.StringToHash("UpperBodyLayer.Swing Left");
    protected int UpperBodyLayer_rightSwingState = Animator.StringToHash("UpperBodyLayer.Swing Right");

    protected virtual void Start() {
        anim = GetComponent<Animator>();
        actor = GetComponent<Actor>();
        anim.SetLayerWeight(upperBodyLayerIndex, 1);
    }

    protected virtual void Update() {
        SetLayerStatesToAnimator();
        ResetOneTimeStates();
    }

    public void SetUpperBodyLayerWeight(int weight) {
        anim.SetLayerWeight(upperBodyLayerIndex, weight);
    }

    private void SetLayerStatesToAnimator() {
        baseLayerState = anim.GetCurrentAnimatorStateInfo(0);
        upperBodyLayerState = anim.GetCurrentAnimatorStateInfo(1);
    }

    private void ResetOneTimeStates() {
        if (upperBodyLayerState.fullPathHash == UpperBodyLayer_LeftSwingState)
            anim.SetBool("Attacking", false);
        if (baseLayerState.fullPathHash == baseLayer_staggerState)
            anim.SetBool("Stagger", false);
        if (baseLayerState.fullPathHash == baseLayer_lungeState)
            anim.SetBool("Lunge", false);
        if (upperBodyLayerState.fullPathHash == UpperBodyLayer_flinchState) 
            anim.SetBool("Flinch", false);

        if (anim.GetBool("DrawArrow") == true) {
            anim.SetBool("DrawArrow", false);
            anim.SetBool("ShootArrow", false);
        }
        
    }

    public void Attack() {
        //if(upperBodyLayerState.fullPathHash != UpperBodyLayer_LeftSwingState)
            anim.SetBool("Attacking", true);
    }

    public void LungeAttack() {
        //if (upperBodyLayerState.fullPathHash != UpperBodyLayer_LeftSwingState)
            anim.SetBool("Lunge", true);
    }

    public void DrawArrow() {
        anim.SetBool("DrawArrow", true);
    }

    public void FireArrow() {
        anim.SetBool("ShootArrow", true);
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
}
