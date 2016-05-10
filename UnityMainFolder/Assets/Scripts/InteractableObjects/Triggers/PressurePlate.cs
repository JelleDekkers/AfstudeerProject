using UnityEngine;
using System.Collections;
using System;

namespace AfstudeerProject.Triggers {

    enum InteractionType {
        PlayerOnly,
        ActorsOnly,
        AIOnly,
        Everything
    }

    public class PressurePlate : Trigger {

        [SerializeField] private InteractionType interactionType;

        private void OnTriggerEnter(Collider col) {
            switch(interactionType) {
                case InteractionType.PlayerOnly:
                    if (col.GetComponent<Player>())
                        InteractWrapper();
                    break;
                case InteractionType.ActorsOnly:
                    if (col.GetComponent<Actor>())
                        InteractWrapper();
                    break;
                case InteractionType.AIOnly:
                    if (col.GetComponent<HumanoidNavHandler>())
                        InteractWrapper();
                    break;
                default:
                    InteractWrapper();
                    break;
            }
        }

        private void InteractWrapper() {
            base.Interact();
            OnInteractedWith();
        }

        protected override void OnInteractedWith() {
            //play sound?
        }
    }
}