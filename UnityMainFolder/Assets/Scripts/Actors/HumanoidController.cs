using UnityEngine;
using System.Collections;

public class HumanoidController : MonoBehaviour {

    protected Animator anim;
    protected float animSpeed = 1;
    protected AnimatorStateInfo baseLayerState;
    protected AnimatorStateInfo upperBodyLayerState;

    protected int baseLayerIndex = 0;
    protected int upperBodyLayerIndex = 1;

    protected int baseLayer_locoState = Animator.StringToHash("Base Layer.Locomotion");
    protected int baseLayer_staggerState = Animator.StringToHash("Base Layer.Stagger"); 
    protected int UpperBodyLayer_flinchState = Animator.StringToHash("UpperBodyLayer.Flinch");
    protected int UpperBodyLayer_nothingState = Animator.StringToHash("UpperBodyLayer.Nothing");
    protected int UpperBodyLayer_LeftSwingState = Animator.StringToHash("UpperBodyLayer.Swing Left");
    protected int UpperBodyLayer_rightSwingState = Animator.StringToHash("UpperBodyLayer.Swing Right");

    public virtual void Start() {
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(upperBodyLayerIndex, 1);
    }

    public virtual void Update() {
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
        if (upperBodyLayerState.fullPathHash == UpperBodyLayer_flinchState) 
            anim.SetBool("Flinch", false);
    }
}
