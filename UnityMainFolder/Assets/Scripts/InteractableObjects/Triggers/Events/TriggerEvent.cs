using UnityEngine;
using System.Collections;

namespace AfstudeerProject.Triggers {

    public abstract class TriggerEvent : MonoBehaviour {

        public float delay = 0f;
        public abstract void ActivateEvent();
    }
}