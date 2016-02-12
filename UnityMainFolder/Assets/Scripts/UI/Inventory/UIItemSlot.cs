using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AfstudeerProject.UI {

    public class UIItemSlot : MonoBehaviour {

        private Sprite emptySlotSprite;
        private Image slotImage;

        public ItemType itemType;
        public ItemData equippedItem { get; private set; }

        private void Start() {
            slotImage = transform.GetChild(0).GetComponent<Image>();
            emptySlotSprite = slotImage.sprite;
            if (equippedItem == null)
                slotImage.sprite = emptySlotSprite;
        }

        public void UpdateSlot(ItemData item) {
            if (item == null) {
                slotImage.sprite = emptySlotSprite;
                GetComponent<Button>().onClick.RemoveAllListeners();
                return;
            } else {
                equippedItem = item;
                slotImage.sprite = equippedItem.Sprite;
                GetComponent<Button>().onClick.AddListener(() => {
                    UIInventory.ActivateOnItemSelected(equippedItem);
                });
            }
        }

        public void ShowItemInfo() {
            if (equippedItem != null)
                UIInventory.ActivateOnItemSelected(equippedItem);
        }
    }
}
