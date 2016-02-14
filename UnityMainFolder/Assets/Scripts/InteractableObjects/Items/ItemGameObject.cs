using UnityEngine;
using System.Collections;
using System;

public class ItemGameObject : InteractableObject {

    protected virtual void OnPickedUp() {
        //play sound?
    }

    public ItemType Type;
    public Sprite Sprite;
    public float Points;

    public string MeshName { get; private set; }

    private void Awake() {
        if(MeshName == null)
            MeshName = gameObject.name;
    }

    public void CopySettingsFromItem(ItemData item) {
        Type = item.Type;
        MeshName = item.MeshName;
        Sprite = item.Sprite;
        Points = item.Points;
    }

    public static void InstantiateFromResourcesFolder(ItemData item) {
        GameObject droppedItem = Instantiate(Resources.Load("Items/" + item.MeshName)) as GameObject;
        Vector3 playerPos = Player.Instance.transform.position;
        droppedItem.transform.position = new Vector3(playerPos.x + UnityEngine.Random.Range(-1, 1), 
                                                     playerPos.y + 1, 
                                                     playerPos.z + UnityEngine.Random.Range(-1, 1));
        float rndRot = UnityEngine.Random.Range(0, 360);
        droppedItem.transform.rotation = Quaternion.Euler(rndRot, rndRot, rndRot);
        droppedItem.GetComponent<ItemGameObject>().CopySettingsFromItem(item);
    }
}
