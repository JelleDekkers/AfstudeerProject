using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AfstudeerProject.UI {

    public class UIItemInfoPanel : MonoBehaviour {

        [SerializeField] private Text nameTxt;
        [SerializeField] private Image img;
        [SerializeField] private Text pointsTxt;
        [SerializeField] private GameObject discardBtn;

        public void UpdateInfo(ItemData item, bool showDiscardedBtn) {
            nameTxt.text = item.Name;
            pointsTxt.text = item.Points.ToString();
            img.sprite = IconManager.GetItemTypeIcon(item);
            //discardBtn.SetActive(showDiscardedBtn);
        }
    }
}