using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameItemInfoPanel : MonoBehaviour {

    [SerializeField] private Text itemNameTxt;
    [SerializeField] private Text itemPointsTxt;
    [SerializeField] private Image itemTypeImg;
    
    public void UpdateInfo(InteractableObject item) {
        itemNameTxt.text = item.Name;
    }
}
