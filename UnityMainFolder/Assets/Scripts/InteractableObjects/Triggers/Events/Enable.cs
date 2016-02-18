using UnityEngine;
using System.Collections;
using System;

namespace AfstudeerProject.Triggers {

    public class Enable : TriggerEvent {

        [SerializeField]
        private GameObject objectToEnable;

        public override void ActivateEvent() {
            StartCoroutine(Toggle());
        }

        private IEnumerator Toggle() {
            yield return new WaitForSeconds(delay);
            if (objectToEnable.activeSelf)
                objectToEnable.SetActive(false);
            else
                objectToEnable.SetActive(true);
        }
    }
}