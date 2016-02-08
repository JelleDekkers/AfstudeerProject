using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UIInventory : MonoBehaviour {

    [SerializeField] private UIInventoryItem inventoryItemPrefab;
    [SerializeField] private Transform inventoryItemsGrid;
    [SerializeField] private Text infoPanelItemName;

    private Item currentSelectedItem;

    private void OnEnable() {
        ShowItems(Player.Inventory.Items.ToArray());
        if(Player.Inventory.Items.Count != 0) {
            currentSelectedItem = Player.Inventory.Items[0];
            ShowItemInfo(currentSelectedItem);
        }
    }

    public void ShowItems(Item[] items) {
        for(int i = 0; i < items.Length; i++) {
            UIInventoryItem obj = Instantiate(inventoryItemPrefab);
            obj.transform.SetParent(inventoryItemsGrid, false);
            obj.Init(this, items[i]);
            if (i == 0)
                EventSystem.current.SetSelectedGameObject(obj.gameObject, new BaseEventData(EventSystem.current));
        }
    }

    public void ShowItemInfo(Item item) {
        currentSelectedItem = item;
        infoPanelItemName.text = item.Name;
    }

    public void DiscardSelectedItem() {
        //drop currentSelectedItem on ground and remove from inventory selected item;
    }
}
