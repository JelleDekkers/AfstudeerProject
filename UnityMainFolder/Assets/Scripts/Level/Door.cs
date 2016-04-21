using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    private bool isOpen;
    private GameObject leftDoor, rightDoor;
    private float speed = 250;
    private float duration = 0.4f;
    private AudioSource audioSource;

    [SerializeField] private bool isLocked;
    [SerializeField] private ItemGameObject key;
    private ItemData keyData;

    private void Start() {
        leftDoor = transform.FindChild("DoorLeft").gameObject;
        rightDoor = transform.FindChild("DoorRight").gameObject;
        speed = Random.Range(speed - 20, speed + 20);
        duration = Random.Range(duration - 0.1f, duration + 0.1f);
        audioSource = GetComponent<AudioSource>();

        if(key != null)
            keyData = new ItemData(key.Name, key.Type, key.MeshName, key.Sprite, key.Points, key.WeaponLength, key.AttackAngle);
    }

    public void DoorTriggered(Actor actor, Vector3 direction) {
        if (isOpen)
            return;

       if(isLocked && key != null && actor.Inventory.Contains(keyData)) {
            isLocked = false;
            // play sound
        }

        if (!isLocked) {
            StartCoroutine(Open(direction));
        } else {
            audioSource.PlayOneShotNormalPitch(AudioManager.Instance.DoorLockedClip);
        }
    }

    private IEnumerator Open(Vector3 direction) {
        float elapsed = 0f;
        audioSource.PlayOneShotNormalPitch(AudioManager.Instance.DoorOpeningClip);
        isOpen = true;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            leftDoor.transform.Rotate(-direction, speed * Time.deltaTime);
            rightDoor.transform.Rotate(direction, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
