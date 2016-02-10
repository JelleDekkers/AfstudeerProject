using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace AfstudeerProject.UI {

    public class UIInventory : MonoBehaviour {

        [SerializeField] private UIInventoryItem inventoryItemPrefab;
        [SerializeField] private Transform inventoryItemsGrid;
        [SerializeField] private InventoryItemInfoPanel infoPanel;

        private Item currentSelectedItem;

        public static event Action<Item> OnItemSelected;
        public static event Action OnSelectedItemIsNull;

        private void OnEnable() {
            if (Player.Inventory.Items.Count != 0)
                ShowItemsInGrid(Player.Inventory.Items.ToArray());
            OnSelectedItemIsNull += OnSelectedItemIsNullFunction;
            OnItemSelected += ShowItemInfo;
        }

        private void Update() {
            if (Input.GetMouseButtonUp(0)) {
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    OnSelectedItemIsNull();
                } else if(EventSystem.current.IsPointerOverGameObject()) {
                    GameObject selectedGObj = EventSystem.current.currentSelectedGameObject;
                    if (selectedGObj == null) {
                        OnSelectedItemIsNull();
                    } else if (selectedGObj.GetComponent<UIItemSlot>() && selectedGObj.GetComponent<UIItemSlot>().equippedItem == null) {
                        OnSelectedItemIsNull();
                    }
                }
            }
        }

        private void OnSelectedItemIsNullFunction() {
            currentSelectedItem = null;
            infoPanel.gameObject.SetActive(false);
        }

        public static void ActivateOnItemSelected(Item item) {
            OnItemSelected(item);
        }

        public void ShowItemsInGrid(Item[] items) {
            if(inventoryItemsGrid.childCount > 0) {
                foreach (Transform t in inventoryItemsGrid.transform)
                    Destroy(t.gameObject);
            }

            for (int i = 0; i < items.Length; i++) {
                UIInventoryItem obj = Instantiate(inventoryItemPrefab);
                obj.transform.SetParent(inventoryItemsGrid, false);
                obj.Init(this, items[i]);
            }
        }

        private void ShowItemInfo(Item item) {
            currentSelectedItem = item;
            infoPanel.gameObject.SetActive(true);
            infoPanel.UpdateInfo(currentSelectedItem);
        }

        public void DiscardSelectedItem() {
            OnSelectedItemIsNull();
            Player.Inventory.RemoveItem(currentSelectedItem);
            ShowItemsInGrid(Player.Inventory.Items.ToArray());
            //rekening houden met update item slot of update item grid
            //drop currentSelectedItem on ground and remove from inventory selected item;
        }
    }
}