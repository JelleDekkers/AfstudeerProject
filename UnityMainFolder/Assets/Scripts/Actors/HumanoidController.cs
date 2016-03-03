using UnityEngine;
using System.Collections;

public class HumanoidController : MonoBehaviour {

    protected Animator anim;
    protected float animSpeed = 1;
    protected AnimatorStateInfo baseLayerState;
    protected AnimatorStateInfo rightArmLayerState;

    protected int baseLayer = 0;
    protected int rightArmLayer = 1;

    protected int baseLayer_locoState = Animator.StringToHash("Base Layer.Locomotion");
    protected int UpperBodyLayer_nothingState = Animator.StringToHash("UpperBodyLayer.Nothing");
    protected int UpperBodyLayer_LeftSwingState = Animator.StringToHash("UpperBodyLayer.Swing Left");
    protected int UpperBodyLayer_rightSwingState = Animator.StringToHash("UpperBodyLayer.Swing Right");

    protected virtual void InitController() {
        anim = GetComponent<Animator>();
        anim.SetLayerWeight(rightArmLayer, 1);
    }
}
