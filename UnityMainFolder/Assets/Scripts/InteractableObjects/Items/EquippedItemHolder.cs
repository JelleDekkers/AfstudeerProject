using UnityEngine;

public class EquippedItemHolder : MonoBehaviour {

    public ItemType Type;

	public void UpdateHolder(ItemData item) {
        // instantiate item
        if (transform.childCount > 0) {
            Destroy(transform.GetChild(0).gameObject);
        }
        if (item != null) {
            GameObject g = Instantiate(Resources.Load("Items/" + item.MeshName)) as GameObject;
            if (g.GetComponent<Rigidbody>())
                Destroy(g.GetComponent<Rigidbody>());
            if (g.GetComponent<Collider>())
                Destroy(g.GetComponent<Collider>());
            if (g.GetComponent<InteractableObject>())
                Destroy(g.GetComponent<InteractableObject>());
            g.transform.SetParent(transform, true);
            g.transform.localPosition = Vector3.zero;
            g.transform.localRotation = Quaternion.Euler(0, 0, 0);
        } else {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
