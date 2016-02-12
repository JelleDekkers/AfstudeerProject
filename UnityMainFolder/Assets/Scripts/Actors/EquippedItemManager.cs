using UnityEngine;
using System.Collections;

public class EquippedItemManager : MonoBehaviour {

    [SerializeField] private EquippedItemHolder helmetHolder;
    [SerializeField] private EquippedItemHolder cuirassHolder;
    [SerializeField] private EquippedItemHolder PauldronLeftHolder;
    [SerializeField] private EquippedItemHolder PauldronRightHolder;
    [SerializeField] private EquippedItemHolder WeaponHolder;
    [SerializeField] private EquippedItemHolder ShieldHolder;
    [SerializeField] private EquippedItemHolder GreaveLeftHolder;
    [SerializeField] private EquippedItemHolder GreaveRightHolder;

    public void ParentItemOnTransform(GameObject item, EquippedItemHolder parent) {
        //parent.UpdateHolder(item);
        //item.transform.SetParent(parent, true);
        //item.transform.localPosition = Vector3.zero;
    }
}
