using UnityEngine;
using System.Collections;

public interface IHittable {
    void Hit(Actor actor, Vector3 direction, float force);
}
