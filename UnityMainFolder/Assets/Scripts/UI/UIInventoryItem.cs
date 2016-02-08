using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInventoryItem : MonoBehaviour {

    private Image img;
    private Item itemRef;

    public void Init(UIInventory parent, Item item) {
        img = transform.GetChild(0).GetComponent<Image>();
        itemRef = item;
        img.sprite = itemRef.Sprite;
        GetComponent<Button>().onClick.AddListener(() => {
            parent.ShowItemInfo(itemRef);
        });
    }
}
