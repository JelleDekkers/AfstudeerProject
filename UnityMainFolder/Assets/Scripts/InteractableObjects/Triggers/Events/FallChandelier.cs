using UnityEngine;
using System.Collections;

namespace AfstudeerProject.Triggers {

    public class FallChandelier : TriggerEvent {

        public override void ActivateEvent() {
            transform.GetComponent<Chandelier>().Fall();
        }
    }
}
