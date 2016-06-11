using UnityEngine;

public class EquippedItemHolder : MonoBehaviour {

    public ItemType Type;
    public GameObject Item;

	public void UpdateHolder(ItemData item, Actor wielder) {
        // instantiate item
        if (transform.childCount > 0) {
            Destroy(transform.GetChild(0).gameObject);
            item = null;
        }

        if (item != null) {
            GameObject g = Instantiate(Resources.Load("Items/" + item.MeshName)) as GameObject;

            OutlineMaterialManager.SwitchToBumpedDiffuse(g);

            if (g.GetComponent<Rigidbody>())
                Destroy(g.GetComponent<Rigidbody>());

            foreach (Collider c in g.GetComponents<Collider>()) {
                c.enabled = false;
            }

            if ((Component)g.GetComponent<IInteractable>())
                Destroy((Component)g.GetComponent<IInteractable>());

            g.transform.SetParent(transform, true);
            g.transform.localPosition = Vector3.zero;
            g.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Common.SetLayerRecursively(Layers.EQUIPPED_ITEM_LAYER, g.transform);

            if (g.GetComponent<ItemGameObject>().Type == ItemType.Shield) {
                g.layer = Layers.SHIELD_LAYER;
            }

            Item = g;
        } else {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
