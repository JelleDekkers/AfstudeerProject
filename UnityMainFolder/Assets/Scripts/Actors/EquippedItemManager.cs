using UnityEngine;
using System.Collections;

public class EquippedItemManager : MonoBehaviour {

    public Transform HeadTransform;
    public Transform ChestTransform;
    public Transform LeftShoulderTransform;
    public Transform RightShoulderTransform;
    public Transform RightHandTransform;
    public Transform LeftHandTransform;
    public Transform LeftLegTransform;
    public Transform RightLegTransform;

    public GameObject shield;
    public GameObject weapon;
    public GameObject leftPauldron;
    public GameObject rightPauldron;
    public GameObject helmet;
    public GameObject cuirass;
    public GameObject leftGreave;
    public GameObject rightGreave;

    public void Start() {
        ParentItemOnTransform(helmet, HeadTransform);
        ParentItemOnTransform(cuirass, ChestTransform);
        ParentItemOnTransform(weapon, RightHandTransform);
        ParentItemOnTransform(shield, LeftHandTransform);
        ParentItemOnTransform(leftPauldron, LeftShoulderTransform);
        ParentItemOnTransform(rightPauldron, RightShoulderTransform);
        ParentItemOnTransform(leftGreave, LeftLegTransform);
        ParentItemOnTransform(rightGreave, RightLegTransform);
    }

    public void ParentItemOnTransform(GameObject item, Transform parent) {
        item.transform.SetParent(parent, true);
        item.transform.localPosition = Vector3.zero;
    }
}
