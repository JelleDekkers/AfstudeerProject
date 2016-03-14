using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour {

    [SerializeField] private float minFlickerIntensity = 1;
    [SerializeField] private float maxFlickerIntensity = 1.1f;
    [SerializeField] private float minFlickerRange = 11;
    [SerializeField] private float maxFlickerRange = 12;
    [SerializeField] private float minFlickerTime = 5;
    [SerializeField] private float maxFlickerTime = 8;

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