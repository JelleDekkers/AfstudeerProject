using UnityEngine;

public abstract class InteractableObject : MonoBehaviour, IInteractable {
    //public abstract void InteractWith();
    public bool IsInteractable { get; private set; }

    [SerializeField] private string objectName = "Object";
    public string Name { get { return objectName; } }

    public virtual void Interact() {

    }
}

public interface IInteractable {
    void Interact();
}
