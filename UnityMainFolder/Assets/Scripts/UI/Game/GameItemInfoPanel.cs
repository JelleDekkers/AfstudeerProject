using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameItemInfoPanel : MonoBehaviour {

    [SerializeField] private Text itemNameTxt;
    [SerializeField] private Text itemPointsTxt;
    [SerializeField] private Image itemTypeImg;
    [SerializeField] private Image itemImg;

    public void UpdateInfo(InteractableObject item) {
        itemNameTxt.text = item.Name;
        if (item.GetComponent<Item>()) {
            itemImg.sprite = item.GetComponent<Item>().Sprite;
            itemPointsTxt.text = item.GetComponent<Item>().Points.ToString();

            if (item.GetComponent<Weapon>())
                itemTypeImg.sprite = IconManager.WeaponIcon;
            if (item.GetComponent<Armor>())
                itemTypeImg.sprite = IconManager.ArmorIcon;
        }
    }
}
