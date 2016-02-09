using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AfstudeerProject.UI {

    public class UIItemSlot : MonoBehaviour {

        public Sprite emptySlotSprite;
        private Image slotImage;
        public Item equippedItem { get; private set; }

        private void Start() {
            slotImage = transform.GetChild(0).GetComponent<Image>();
            if (equippedItem == null)
                slotImage.sprite = emptySlotSprite;
        }

        public void UpdateSlot(Item item) {
            slotImage.sprite = item.Sprite;
            //Player.equippedItemManager.ParentItemOnTransform(item)
            GetComponent<Button>().onClick.AddListener(() => {
                UIInventory.ActivateOnItemSelected(equippedItem);
            });
        }
    }
}
