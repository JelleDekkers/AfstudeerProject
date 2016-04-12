using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AfstudeerProject.Triggers;

public class GameItemInfoPanel : MonoBehaviour {

    [SerializeField] private Text itemNameTxt;
    [SerializeField] private Text itemPointsTxt;
    [SerializeField] private Image itemTypeImg;
    [SerializeField] private Image itemImg;

    public void UpdateInfo(InteractableObject item) {
        if (item == null)
            return;

        if (item.GetComponent<ItemGameObject>()) {
            itemNameTxt.text = item.Name;
            itemImg.sprite = item.GetComponent<ItemGameObject>().Sprite;
            itemPointsTxt.text = item.GetComponent<ItemGameObject>().Points.ToString();

            if (item.GetComponent<ItemGameObject>())
                itemTypeImg.sprite = IconManager.GetItemTypeIcon(item.GetComponent<ItemGameObject>());
        } else if(item.GetComponent<Lever>()){
            itemNameTxt.text = "Use " + item.Name;
        } else if(item.GetComponent<Chest>()) {
            itemNameTxt.text = "Open " + item.Name;
        }

        itemPointsTxt.enabled = item.GetComponent<ItemGameObject>();
        itemTypeImg.enabled = item.GetComponent<ItemGameObject>();
        itemImg.enabled = item.GetComponent<ItemGameObject>();
    }
}
