﻿using UnityEngine;
using System.Collections;

public class Shaker : MonoBehaviour {

    public Vector3 PositionScale = new Vector3(1, 1, 0);
    public Vector3 RotationScale = new Vector3(0, 0, 0);
    [Range(1, 50)]
    public int shakeSpeed = 1;

    Vector3 origin;
    Quaternion originRotation;
    Coroutine co = null;

    void Start() {
        Shake(50, 2);
    }

    /// <summary>
    /// Shake at POWER for DURATION seconds. Multiple calls will yield most recent only.
    /// </summary>
    /// <param name="power">How violent to shake (0-50)</param>
    /// <param name="duration">Duration of shaking</param>
    public void Shake(int power, float duration) {
        if (co != null) {
            StopCoroutine(co);
            RevertOrigin();
        }
        shakeSpeed = power;
        co = StartCoroutine(DoShake(duration));
    }

    /// <summary>
    /// Set current position and rotation as 'origin'
    /// </summary>
    void SetOrigin() {
        origin = transform.position;
        originRotation = transform.rotation;
    }

    /// <summary>
    /// Revert the transform to the original position and rotation
    /// </summary>
    void RevertOrigin() {
        transform.position = origin;
        transform.rotation = originRotation;
        co = null;
    }

    IEnumerator DoShake(float duration) {
        SetOrigin();
        float speed = shakeSpeed / 200f;
        // Start on a 0.5f value (center) for smooth transition into shaking.
        Vector2 noise = new Vector2(Random.value * 10, Random.value * 100);
        float factor = Mathf.PerlinNoise(noise.x, noise.y);
        while (factor > 0.525f || factor < 0.475f) {
            noise.x += 0.05f;
            factor = Mathf.PerlinNoise(noise.x, noise.y);
        }
        // Shake for DURATION
        Vector3 offset = origin;
        Vector3 q = originRotation.eulerAngles;
        float step = 0;
        float elapsed = 0;
        while (elapsed < duration) {
            // Generate unique perlin noise, only if needed
            if (PositionScale.x != 0)
                offset.x = (Mathf.PerlinNoise(noise.x + step, noise.y) * 2f - 1f) * PositionScale.x;
            if (PositionScale.y != 0)
                offset.y = (Mathf.PerlinNoise(noise.x + step * 0.41f, noise.y + step * 0.41f) * 2f - 1f) * PositionScale.y;
            if (PositionScale.z != 0)
                offset.z = (Mathf.PerlinNoise(noise.x, noise.y + step) * 2f - 1f) * PositionScale.z;
            if (RotationScale.x != 0)
                q.x = (Mathf.PerlinNoise(noise.x - step, noise.y) * 2f - 1f) * RotationScale.x;
            if (RotationScale.y != 0)
                q.y = (Mathf.PerlinNoise(noise.x - step * 0.41f, noise.y - step * 0.41f) * 2f - 1f) * RotationScale.y;
            if (RotationScale.z != 0)
                q.z = (Mathf.PerlinNoise(noise.x, noise.y - step) * 2f - 1f) * RotationScale.z;
            // Update transform
            transform.rotation = Quaternion.Euler(originRotation.eulerAngles + q);
            transform.position = origin + offset;
            step += speed;
            elapsed += Time.deltaTime;
            yield return null;
        }
        // Smoothly return to start (this bleeds over the given 'duration')
        float dt = 0;
        Vector3 at = transform.position;
        Quaternion rot = transform.rotation;
        while (dt < 1) {
            transform.position = Vector3.Lerp(at, origin, dt);
            transform.rotation = Quaternion.Lerp(rot, originRotation, dt);
            dt += shakeSpeed / 50f;
            yield return null;
        }
        RevertOrigin();
    }

}