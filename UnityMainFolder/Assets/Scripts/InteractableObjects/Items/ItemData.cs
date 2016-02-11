using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemData {
    public string Name { get; private set; }
    public ItemType Type { get; private set; }
    public string MeshName { get; private set; }
    public Sprite Sprite { get; private set; }
    public float Points { get; private set; }

    public ItemData(string name, ItemType type, string meshName, Sprite sprite, float points) {
        Name = name;
        Type = type;
        MeshName = meshName;
        Sprite = sprite;
        Points = points;
    }
}
