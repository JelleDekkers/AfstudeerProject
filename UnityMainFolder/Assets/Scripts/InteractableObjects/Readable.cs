using UnityEngine;
using System.Collections;

public class Readable : MonoBehaviour, IInteractable {

	[SerializeField] private string text;

    public void Interact() {
        GameUI.Instance.ShowReadObject(text);
        Time.timeScale = 0;
    }
}
