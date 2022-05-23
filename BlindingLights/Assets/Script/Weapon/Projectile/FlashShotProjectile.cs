using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashShotProjectile : ProjectileBase
{
    public GameObject flashLight;
    public float timerTillDestroy = 5f;

    protected override void Awake()
    {
        flashLight.SetActive(false);
        base.Awake();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == Owner)
        {
            return;
        }

        //Disable Movement
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero; // projectile won't move when in the ground


        flashLight.SetActive(true); // enable Light
        Destroy(gameObject, timerTillDestroy); // destroy in 5 seconds
    }
}
