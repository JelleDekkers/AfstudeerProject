using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace AfstudeerProject.MiniMap {

    public class MiniMap : MonoBehaviour {

        [SerializeField] private DrawObject[] objectsToDraw;

        private List<GameObject> nonStaticObjects = new List<GameObject>();

        private Transform iconParent;

        private void Start() {
            iconParent = transform.Find("Icons");

            foreach(DrawObject obj in objectsToDraw) { 
                GameObject parent = new GameObject();
                parent.name = obj.tagString;
                parent.transform.SetParent(iconParent);
                parent.AddComponent<RectTransform>().position = transform.position;
                GameObject[] GameObjectsWithTag = GameObject.FindGameObjectsWithTag(obj.tagString);
                foreach (GameObject g in GameObjectsWithTag) {
                    DrawIconOnMap(obj.sprite, parent.transform, g.transform);

                    if (obj.isStatic == false)
                        nonStaticObjects.Add(g);

                    //add component die automatisch positie volgt? ipv hier in update
                }
            }
        }

        private void Update() {
            //foreach(GameObject g in nonStaticObjects) {
            //    Vector3 screenPos = new Vector3(g.transform.position.x, g.transform.position.z, 0);
            //    g.GetComponent<RectTransform>().localPosition = screenPos;
            //}
        }

        private void DrawIconOnMap(Sprite sprite, Transform parent, Transform objectTransform) {
            GameObject g = new GameObject();
            Image img = g.AddComponent<Image>();
            img.sprite = sprite;
            img.preserveAspect = true;
            g.transform.SetParent(parent, false);
            g.name = parent.name;
            g.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            g.transform.rotation = Quaternion.Euler(g.transform.rotation.x, g.transform.rotation.z, objectTransform.rotation.y + 90);

            Vector3 screenPos = new Vector3(objectTransform.position.x, objectTransform.position.z, 0);
            g.GetComponent<RectTransform>().localPosition = screenPos;

            //Vector2 viewportPoint = Camera.main.WorldToViewportPoint(worldPosition); 
        }
    }

    [System.Serializable]
    public class DrawObject {
        public string tagString;
        public Sprite sprite;
        public bool isStatic;
    }
}