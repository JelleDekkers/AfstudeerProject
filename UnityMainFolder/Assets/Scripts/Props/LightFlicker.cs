using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour {

    [SerializeField] private float minFlickerIntensity = 0.9f;
    [SerializeField] private float maxFlickerIntensity = 1.1f;
    [SerializeField] private float minFlickerRange = 2;
    [SerializeField] private float maxFlickerRange = 14;
    [SerializeField] private float minFlickerTime = 0.01f;
    [SerializeField] private float maxFlickerTime = 0.1f;

    private Light lightSource;
    private float randomizer = 0;
    private float timer;

    private void Start() {
        lightSource = GetComponent<Light>();
        timer = Random.Range(minFlickerTime, maxFlickerTime);
    }

    private void Update() {
        if (timer > 0)
            timer--;
        if(timer <= 0) {
            lightSource.intensity = (Random.Range(minFlickerIntensity, maxFlickerIntensity));
            lightSource.range = (Random.Range(minFlickerRange, maxFlickerRange));
            randomizer = Random.Range(0, 1.1f);
            timer = Random.Range(minFlickerTime, maxFlickerTime);
        }
    }

}