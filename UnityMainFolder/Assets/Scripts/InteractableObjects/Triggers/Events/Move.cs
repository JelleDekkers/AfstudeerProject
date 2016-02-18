using UnityEngine;
using System.Collections;

namespace AfstudeerProject.Triggers {

    public class Move : TriggerEvent {

        [SerializeField]
        private Vector3 direction;
        [SerializeField]
        private float speed = 1f;

        public override void ActivateEvent() {
            Vector3 targetPos = gameObject.transform.position + direction;
            StartCoroutine(MoveFromTo(gameObject.transform, gameObject.transform.position, targetPos, speed));
        }

        IEnumerator MoveFromTo(Transform objectToMove, Vector3 a, Vector3 b, float speed) {
            float step = (speed / (a - b).magnitude) * Time.fixedDeltaTime;
            float t = 0;
            while (t <= 1.0f) {
                t += step; // Goes from 0 to 1, incrementing by step each time
                objectToMove.position = Vector3.Lerp(a, b, t); // Move objectToMove closer to b
                yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
            }
            objectToMove.position = b;
        }
    }
}