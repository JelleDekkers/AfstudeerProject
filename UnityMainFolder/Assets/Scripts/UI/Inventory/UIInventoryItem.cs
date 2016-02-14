using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AfstudeerProject.UI {

    public class UIInventoryItem : MonoBehaviour {

        private Image img;
        private Text countTxt;

        public List<ItemData> Items = new List<ItemData>();

        public void Init(List<ItemData> itemsList) {
            if (img == null)
                img = transform.GetChild(0).GetComponent<Image>();
            if (countTxt == null)
                countTxt = transform.GetChild(1).GetComponent<Text>();

            Items = itemsList;
            img.sprite = Items[0].Sprite;

            if (Items.Count == 1)
                countTxt.gameObject.SetActive(false);
            else {
                countTxt.gameObject.SetActive(true);
                countTxt.text = "x" + Items.Count.ToString();
            }

            GetComponent<Button>().enabled = true;
            GetComponent<Button>().onClick.AddListener(() => {
                UIInventoryManager.ActivateOnItemSelected(Items[0], true);
            });
        }
    }
}