using UnityEngine;
using System.Collections;

public class Readable : InteractableObject {

	[SerializeField] private string text;

    public override void Interact() {
        base.Interact();
        GameUI.Instance.ShowReadObject(text);
        Time.timeScale = 0;
    }
}
