using UnityEngine;
using System.Collections;

public class EquippedItemManager : MonoBehaviour {

    public EquippedItemHolder helmetHolder;
    public EquippedItemHolder cuirassHolder;
    public EquippedItemHolder PauldronLeftHolder;
    public EquippedItemHolder PauldronRightHolder;
    public EquippedItemHolder WeaponHolder;
    public EquippedItemHolder ShieldHolder;
    public EquippedItemHolder GreaveLeftHolder;
    public EquippedItemHolder GreaveRightHolder;

    public void ParentItemOnTransform(GameObject item, EquippedItemHolder parent) {
        //parent.UpdateHolder(item);
        //item.transform.SetParent(parent, true);
        //item.transform.localPosition = Vector3.zero;
    }

    public float GetTotalArmorPoints() {
        return 2;
    }

    public float GetTotalAttackPoints() {
        return 2;
    }

    public float GetTotalBlockPoints() {
        return 2;
    }
}
