using UnityEngine;
using System.Collections;
using System;

namespace AfstudeerProject.Triggers {

    public class ShowHiddenRoom : TriggerEvent {

        [SerializeField]
        private GameObject hiddenRoom;

        public override void ActivateEvent() {
            StartCoroutine(ShowRoom());
        }

        private IEnumerator ShowRoom() {
            yield return new WaitForSeconds(delay);
            LevelManager.HiddenRoomsFound++;
            Debug.Log("Hidden Room Found!");
            hiddenRoom.SetActive(true);
        }
    }
}