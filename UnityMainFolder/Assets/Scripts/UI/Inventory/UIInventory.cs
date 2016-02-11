using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace AfstudeerProject.UI {

    public class UIInventory : MonoBehaviour {

        [SerializeField] private UIInventoryItem inventoryItemPrefab;
        [SerializeField] private Transform inventoryItemsGrid;
        [SerializeField] private InventoryItemInfoPanel infoPanel;
        [SerializeField] private Text armorPointsText;
        [SerializeField] private Text attackPointsText;
        [SerializeField] private Text healthPointsText;

        private ItemData currentSelectedItem;
        //private type typeItemShown;
        private ItemData[] itemsBeingShown;

        private static UIInventory instance;
        public static event Action<ItemData> OnItemSelected;
        public static event Action OnSelectedItemIsNull;

        private void Awake() {
            if (instance == null)
                instance = this;
        }

        private void OnEnable() {
            if (Player.Inventory.Items.Count != 0)
                ShowItemsInGrid(Player.Inventory.Items.ToArray());
            OnSelectedItemIsNull += OnSelectedItemIsNullFunction;
            OnItemSelected += ShowItemInfo;

            armorPointsText.text = Player.ArmorPoints.ToString();
            attackPointsText.text = Player.AttackPoints.ToString();
            healthPointsText.text = Player.HealthPoints.ToString();
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

        public static void ActivateOnItemSelected(ItemData item) {
            OnItemSelected(item);
        }

        public void ShowItemsInGrid(ItemData[] items) {
            itemsBeingShown = items;
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

        public static void UpdateItemsGrid() {
            // TODO: dynamisch maken
            instance.ShowItemsInGrid(Player.Inventory.Items.ToArray());
        }

        private void ShowItemInfo(ItemData item) {
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