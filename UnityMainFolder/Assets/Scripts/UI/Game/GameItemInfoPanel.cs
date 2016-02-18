using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameItemInfoPanel : MonoBehaviour {

    [SerializeField] private Text itemNameTxt;
    [SerializeField] private Text itemPointsTxt;
    [SerializeField] private Image itemTypeImg;
    [SerializeField] private Image itemImg;

    public void UpdateInfo(InteractableObject item) {
        if (item == null)
            return;

        itemNameTxt.text = item.Name;
        if (item.GetComponent<ItemGameObject>()) {
            itemImg.sprite = item.GetComponent<ItemGameObject>().Sprite;
            itemPointsTxt.text = item.GetComponent<ItemGameObject>().Points.ToString();

            if (item.GetComponent<ItemGameObject>())
                itemTypeImg.sprite = IconManager.GetItemTypeIcon(item.GetComponent<ItemGameObject>());
        }
    }
}
