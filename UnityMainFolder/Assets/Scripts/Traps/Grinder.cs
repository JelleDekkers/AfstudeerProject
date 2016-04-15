﻿using UnityEngine;
using System.Collections;
using System;

public class Grinder : MonoBehaviour, ITrap {

    [SerializeField] private float playerDamage;
    [SerializeField] private float aIDamage;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 rotationDirection;

    public bool IsBroken { get; private set; }

    private void Update() {
        if (IsBroken) {
            gameObject.SetActive(false);
            return;
        }

        transform.Rotate(rotationDirection * Time.deltaTime * rotationSpeed);
    }

    public void OnTriggered(Actor actor) {
        if (actor.GetType() == typeof(Player))
            actor.TakeDamage(gameObject, playerDamage);
        else
            actor.TakeDamage(gameObject, aIDamage);
    }
}
