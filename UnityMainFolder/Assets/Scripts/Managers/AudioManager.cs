using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    public AudioClip PickUpObjectClip;
    public AudioClip HitActorClip;
    public AudioClip DoorOpeningClip;
    public AudioClip DoorLockedClip;

    private void Start() {
        if (Instance == null)
            Instance = this;
    }
}
