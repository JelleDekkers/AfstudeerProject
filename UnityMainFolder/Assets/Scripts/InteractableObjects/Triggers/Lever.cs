using UnityEngine;
using System.Collections;
using System;

namespace AfstudeerProject.Triggers {

    public class Lever : Trigger {

        public override void Interact() {
            base.Interact();
            OnInteractedWith();
        }

        protected override void OnInteractedWith() {
            //play sound?
        }
    }
}