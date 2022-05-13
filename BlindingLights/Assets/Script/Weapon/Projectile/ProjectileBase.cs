using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    Rigidbody rb = null;

    [SerializeField] float Velocity = 20; // higher the velocity, the faster the projectile is

    public GameObject Owner = null; // set from WeaponBase

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        //adding initial movement force                     Will not keep physics when going to other direction
        rb.AddForce(transform.forward * Velocity, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Owner)
        {
            return;
        }

        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            GameplayStatics.DealDamage(other.gameObject, 100); // Instant Big Damage so that enemy will die from a hit
        }

        Destroy(gameObject);
    }
}
