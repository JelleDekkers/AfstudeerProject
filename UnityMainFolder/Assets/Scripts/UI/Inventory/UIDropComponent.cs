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

            if (receivingImage == null)
                return;

            ItemData draggedItem = GetDraggedItem(data);
            // Put item into ItemSlot:
            if (draggedItem.Type == GetComponent<UIItemSlot>().itemType) {
                if(UIDragComponent.m_DraggingIcon!= null)
                    Destroy(UIDragComponent.m_DraggingIcon);

                Sprite dropSprite = GetDropSprite(data);
                if (dropSprite != null)
                    receivingImage.sprite = dropSprite;

                GetComponent<UIItemSlot>().UpdateSlot(draggedItem);

                Player.Inventory.RemoveItem(draggedItem);
                UIInventory.UpdateItemsGrid();
            } //else if getComponent<grid>
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

            return data.pointerDrag.GetComponent<UIInventoryItem>().ItemRef;
        }
    }
}
