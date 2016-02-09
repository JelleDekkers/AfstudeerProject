using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIItemSlot : MonoBehaviour {

    public Sprite emptySlotSprite;
    private Image slotImage;
    private Item equippedItem;

    private void Start() {
        if (equippedItem == null)
            slotImage.sprite = emptySlotSprite;
    }

    public void UpdateSlot(Item item) {
        slotImage.sprite = item.Sprite;
    }
}
