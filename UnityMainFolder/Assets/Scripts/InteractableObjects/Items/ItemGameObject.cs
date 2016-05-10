using UnityEngine;
using System.Collections;
using System;

public class ItemGameObject : MonoBehaviour, IInteractable {

    public string Name;
    public ItemType Type;
    public Sprite Sprite;
    public float Points;
    public float WeaponLength = 1;
    public float AttackAngle = 30;

    public string MeshName {
        get { return name; }
        private set { MeshName = value; }
    }

    private void Awake() {
        if(MeshName == null)
            MeshName = gameObject.name;
    }

    public void Interact() {
        OnPickedUp();
    }

    protected void OnPickedUp() {
        //play sound?
    }

    public static void InstantiateFromResourcesFolder(ItemData item) {
        GameObject droppedItem = Instantiate(Resources.Load("Items/" + item.MeshName)) as GameObject;
        Vector3 playerPos = Player.Instance.transform.position;
        droppedItem.transform.position = new Vector3(playerPos.x + UnityEngine.Random.Range(-1, 1), 
                                                     playerPos.y + 1, 
                                                     playerPos.z + UnityEngine.Random.Range(-1, 1));
        float rndRot = UnityEngine.Random.Range(0, 360);
        droppedItem.transform.rotation = Quaternion.Euler(rndRot, rndRot, rndRot);
        //droppedItem.GetComponent<ItemGameObject>().CopySettingsFromItem(item);
    }
}
