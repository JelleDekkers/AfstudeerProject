using UnityEngine;
using UnityEngine.UI;

namespace AfstudeerProject.UI {

    public class UIItemSlot : MonoBehaviour {

        private Sprite emptySlotSprite;
        private Image slotImage;

        public ItemType itemType;
        public ItemData equippedItem { get; private set; }

        public static event System.Action OnItemSlotChanged;

        private void Start() {
            slotImage = transform.GetChild(0).GetComponent<Image>();
            emptySlotSprite = slotImage.sprite;
            if (equippedItem == null)
                slotImage.sprite = emptySlotSprite;
        }

        public void UpdateSlot(ItemData item) {
            if (item == null) {
                slotImage.sprite = emptySlotSprite;
                equippedItem = null;
                //Player.Inventory.
                GetComponent<Button>().onClick.RemoveAllListeners();
                return;
            } else {
                if (equippedItem != null) {
                    Player.Inventory.AddItem(equippedItem);
                }
                equippedItem = item;
                slotImage.sprite = equippedItem.Sprite;
                Player.Inventory.EquipItem(item);
                GetComponent<Button>().onClick.RemoveAllListeners();
                GetComponent<Button>().onClick.AddListener(() => {
                    UIInventoryManager.ActivateOnItemSelected(equippedItem, false);
                });
            }
            OnItemSlotChanged();
        }

        public void ShowItemInfo() {
            if (equippedItem != null)
                UIInventoryManager.ActivateOnItemSelected(equippedItem, false);
        }
    }
}
