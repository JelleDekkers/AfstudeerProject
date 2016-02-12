using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace AfstudeerProject.UI {

    public class UIInventory : MonoBehaviour {

        public enum shownItemsCategory {
            All,
            Weapons,
            Shields,
            Armor,
            Misc
        }

        [SerializeField] private UIInventoryGrid inventoryGrid;
        [SerializeField] private InventoryItemInfoPanel itemInfoPanel;
        [SerializeField] private UIPlayerInfoPanel playerInfoPanel;

        private ItemData currentSelectedItem;

        public static shownItemsCategory ShownCategory = shownItemsCategory.All;
        public static ItemData[] itemsBeingShown;

        private static UIInventory instance;
        public static event Action<ItemData> OnItemSelected;
        public static event Action OnSelectedItemIsNull;

        private void Awake() {
            if (instance == null)
                instance = this;
        }

        private void OnEnable() {
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
            itemInfoPanel.gameObject.SetActive(false);
        }

        public static void ActivateOnItemSelected(ItemData item) {
            OnItemSelected(item);
        }

        public void ShowItemsInGrid(ItemData[] items) {
            inventoryGrid.ShowItemsInGrid(items);
        }

        public static void UpdateItemsGrid() {
            // TODO: dynamisch maken
            instance.inventoryGrid.ShowItemsInGrid(Player.Inventory.Items.ToArray());
        }

        private void ShowItemInfo(ItemData item) {
            currentSelectedItem = item;
            itemInfoPanel.gameObject.SetActive(true);
            itemInfoPanel.UpdateInfo(currentSelectedItem);
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