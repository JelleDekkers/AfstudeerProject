using UnityEngine;
using System.Collections;

namespace AfstudeerProject.Triggers {

    public enum TriggerType {
        Once,
        Toggle
    }

    public abstract class Trigger : InteractableObject {

        public TriggerType Type;
        public bool CanBeTriggered = true;

        public TriggerEvent[] triggeredObjects;

        public override void Interact() {
            if (Type == TriggerType.Once) {
                gameObject.layer = 0; // Default Layer
                CanBeTriggered = false;
            }

            foreach (TriggerEvent t in triggeredObjects) {
                t.ActivateEvent();
            }
        }

        protected abstract void OnInteractedWith();
    }
}