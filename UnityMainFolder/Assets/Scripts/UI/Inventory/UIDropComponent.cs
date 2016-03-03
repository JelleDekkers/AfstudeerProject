using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AfstudeerProject.UI {

    public class UIDropComponent : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
        public Image containerImage; // The drop box
        public Image receivingImage; // The image that will change
        private Color normalColor;
        public Color highlightColor = Color.yellow;

        public void OnEnable() {
            if (containerImage != null)
                normalColor = containerImage.color;
        }

        public void OnDrop(PointerEventData data) {
            containerImage.color = normalColor;

            if (receivingImage == null && !GetComponent<GridLayoutGroup>())
                return;
            
            ItemData draggedItem = GetDraggedItem(data);
            // Used to check if being dragged from grid or from itemslot
            UIItemSlot itemSlot = GetItemSlotBeingDraggedFrom(data);

            // Put item from inventory into valid ItemSlot:
            if (GetComponent<UIItemSlot>() && draggedItem.Type == GetComponent<UIItemSlot>().itemType) {
                if(UIDragComponent.m_DraggingIcon!= null)
                    Destroy(UIDragComponent.m_DraggingIcon);

                Sprite dropSprite = GetDropSprite(data);
                if (dropSprite != null)
                    receivingImage.sprite = dropSprite;

                GetComponent<UIItemSlot>().UpdateSlot(draggedItem);
                Player.Instance.Inventory.RemoveItem(draggedItem);
                UIInventoryManager.UpdateItemsGrid();
                UIInventoryManager.OnItemMovedFunction();
            } // Put Item from item slot back to inventory: 
            else if(GetComponent<UIInventoryGrid>() && itemSlot != null && itemSlot.equippedItem != null) {
                Player.Instance.Inventory.AddItem(draggedItem);
                UIInventoryManager.UpdateItemsGrid();
                itemSlot.UpdateSlot(null);
                UIInventoryManager.OnItemMovedFunction();
            }
        }

        public void OnPointerEnter(PointerEventData data) {
            if (containerImage == null)
                return;

            Sprite dropSprite = GetDropSprite(data);
            if (dropSprite != null)
                containerImage.color = highlightColor;
        }

        public void OnPointerExit(PointerEventData data) {
            if (containerImage == null)
                return;

            containerImage.color = normalColor;
        }

        private Sprite GetDropSprite(PointerEventData data) {
            var originalObj = data.pointerDrag;
            if (originalObj == null)
                return null;

            var childImg = originalObj.transform.GetChild(0).GetComponent<Image>();
            if (childImg == null)
                return null;

            return childImg.sprite;
        }

        private ItemData GetDraggedItem(PointerEventData data) {
            var originalObj = data.pointerDrag;
            if (originalObj == null)
                return null;

            if (data.pointerDrag.GetComponent<UIInventoryItem>())
                return data.pointerDrag.GetComponent<UIInventoryItem>().Item.Item;
            else
                return data.pointerDrag.GetComponent<UIItemSlot>().equippedItem;
        }

        private UIItemSlot GetItemSlotBeingDraggedFrom(PointerEventData data) {
            var originalObj = data.pointerDrag;
            if (originalObj == null)
                return null;

            return data.pointerDrag.GetComponent<UIItemSlot>();
        }
    }
}
