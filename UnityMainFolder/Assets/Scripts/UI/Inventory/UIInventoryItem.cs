using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AfstudeerProject.UI {

    public class UIInventoryItem : MonoBehaviour {

        private Image img;
        private Text countTxt;

        public InventoryItem Item;

        public void Init(InventoryItem item) {
            if (img == null)
                img = transform.GetChild(0).GetComponent<Image>();
            if (countTxt == null)
                countTxt = transform.GetChild(1).GetComponent<Text>();

            Item = item;
            img.sprite = Item.Item.Sprite;

            if (Item.Count == 1)
                countTxt.gameObject.SetActive(false);
            else {
                countTxt.gameObject.SetActive(true);
                countTxt.text = "x" + Item.Count.ToString();
            }

            GetComponent<Button>().enabled = true;
            GetComponent<Button>().onClick.AddListener(() => {
                UIInventoryManager.ActivateOnItemSelected(Item.Item, true);
            });
        }
    }
}