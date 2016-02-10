using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AfstudeerProject.UI {

    public class UIInventoryItem : MonoBehaviour {

        private Image img;

        public Item ItemRef { get; private set; }

        public void Init(UIInventory parent, Item item) {
            if(img == null)
                img = transform.GetChild(0).GetComponent<Image>();

            ItemRef = item;
            img.sprite = ItemRef.Sprite;
            GetComponent<Button>().enabled = true;
            GetComponent<Button>().onClick.AddListener(() => {
                UIInventory.ActivateOnItemSelected(ItemRef);
            });
        }
    }
}