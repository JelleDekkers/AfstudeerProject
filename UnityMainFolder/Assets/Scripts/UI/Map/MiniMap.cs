using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace AfstudeerProject.MiniMap {

    public class MiniMap : MonoBehaviour {

        [SerializeField] private Transform northMarker;

        private void Start() {
            if (northMarker == null)
                Debug.LogWarning("No North Marker given");
        }

        private void Update() {
            if(northMarker != null)
                northMarker.eulerAngles = new Vector3(0, 0, Player.Instance.transform.eulerAngles.y);
        }
    }
}