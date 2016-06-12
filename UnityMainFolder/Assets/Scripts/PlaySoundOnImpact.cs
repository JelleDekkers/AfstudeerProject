using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnImpact : MonoBehaviour {

    [SerializeField] private AudioClip[] clips;

    private const float VELOCITY_MININUM = 5;
    private AudioSource source;

    private void Start() {
        source = GetComponent<AudioSource>();
    }

	private void OnCollisionEnter(Collision col) {
        if (col.relativeVelocity.magnitude >= VELOCITY_MININUM) {
            //AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], col.contacts[0].point, 2);
            source.PlayOneShot(clips[Random.Range(0, clips.Length)], 0.2f);
        }
    }
}
