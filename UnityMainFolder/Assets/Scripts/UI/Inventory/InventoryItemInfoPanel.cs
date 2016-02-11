using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AfstudeerProject.UI {

    public class InventoryItemInfoPanel : MonoBehaviour {

        [SerializeField] private Text nameTxt;

        public void UpdateInfo(ItemData item) {
            if(nameTxt == null)
                nameTxt = transform.GetChild(0).GetComponent<Text>();

            nameTxt.text = item.Name;
        }
    }
}