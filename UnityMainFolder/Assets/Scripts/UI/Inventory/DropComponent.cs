using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AfstudeerProject.UI {

    public class DropComponent : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
        public Image containerImage; // The drop box
        public Image receivingImage; // The image that will change
        private Color normalColor;
        public Color highlightColor = Color.yellow;

        public void OnEnable() {
            if (containerImage != null)
                normalColor = containerImage.color;
        }

        public void OnDrop(PointerEventData data) {
            //if(GetComponent<UIItemSlot>()) is !valid return
            print("OnDrop()");
            containerImage.color = normalColor;

            if (receivingImage == null)
                return;

            Sprite dropSprite = GetDropSprite(data);
            if (dropSprite != null) {
                //if (transform.GetChild(0).GetComponent<GridLayoutGroup>()) {
                    
                //} else {
                //    print("no grid detected");
                    receivingImage.sprite = dropSprite;
                //}
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

            var srcImage = originalObj.GetComponent<Image>();
            if (srcImage == null)
                return null;

            return srcImage.sprite;
        }
    }
}
