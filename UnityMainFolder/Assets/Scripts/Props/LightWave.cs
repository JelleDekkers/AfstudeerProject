using UnityEngine;
using System.Collections;

public class LightWave : MonoBehaviour {

    public float offset = 0.0f; // constant offset
    public float amplitude = 1.0f; // amplitude of the wave
    public float phase = 0.0f; // start point inside on wave cycle
    public float frequency = 0.5f; // cycle frequency per second
    public bool affectsIntensity = true;

    // Keep a copy of the original values
    private Color originalColor;
    private float originalIntensity;

    // Use this for initialization
    void Start() {
        originalColor = GetComponent<Light>().color;
        originalIntensity = GetComponent<Light>().intensity;
    }

    // Update is called once per frame
    void Update() {
        Light light = GetComponent<Light>();
        if (affectsIntensity)
            light.intensity = originalIntensity * EvalWave();

        Color o = originalColor;
        Color c = GetComponent<Light>().color;

        light.color = originalColor * EvalWave();  
    }

    private float EvalWave() {
        float x = (Time.time + phase) * frequency;
        float y;
        x = x - Mathf.Floor(x);
        y = Mathf.Sin(x * 4 * Mathf.PI);
        return (y * amplitude) + offset;
    }
}