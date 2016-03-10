using UnityEngine;

public class EquippedItemHolder : MonoBehaviour {

    public ItemType Type;
    public GameObject Item;

    private int shieldLayer = 16;

	public void UpdateHolder(ItemData item, Actor wielder) {
        // instantiate item
        if (transform.childCount > 0) {
            Destroy(transform.GetChild(0).gameObject);
            item = null;
        }

        if (item != null) {
            GameObject g = Instantiate(Resources.Load("Items/" + item.MeshName)) as GameObject;

            if (g.GetComponent<Rigidbody>())
                Destroy(g.GetComponent<Rigidbody>());

            foreach (Collider c in g.GetComponents<Collider>()) {
                if (!c.isTrigger)
                    Destroy(c);
            }

            if (g.GetComponent<InteractableObject>())
                Destroy(g.GetComponent<InteractableObject>());
            g.transform.SetParent(transform, true);
            g.transform.localPosition = Vector3.zero;
            g.transform.localRotation = Quaternion.Euler(0, 0, 0);
            g.layer = 13;// equippedItem Layer

            if (g.GetComponent<ItemGameObject>().Type == ItemType.Shield) {
                g.AddComponent<EquippedShield>().Init(wielder);
                g.layer = 16;
            }

            Item = g;
        } else {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
