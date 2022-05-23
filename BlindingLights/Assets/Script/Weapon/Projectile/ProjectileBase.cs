using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb = null;

    [SerializeField] float Velocity = 20; // higher the velocity, the faster the projectile is

    public GameObject Owner = null; // set from WeaponBase

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        MoveTowardsReticle();
        // Rotating the projectile also need Add Force
        //adding initial movement force                     Will not keep physics when going to other direction
        rb.AddForce(transform.forward * Velocity, ForceMode.VelocityChange);
    }

    public virtual void MoveTowardsReticle()
    {
        //Get Camera references
        GameObject Player = GameManager.Instance.GetPlayer();
        Transform playerCamera = Player.transform.Find("Main Camera");

        // Create local RaycastHit Var
        RaycastHit hitInfo;

        //Raycast to the camera
        Physics.Raycast(playerCamera.position, playerCamera.forward, out hitInfo);

        //Rotate the projectile to the raycast hit position
        gameObject.transform.LookAt(hitInfo.point);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if(other.gameObject == Owner)
        {
            return;
        }

        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            Score.scoreValue += 10;
            GameplayStatics.DealDamage(other.gameObject, 100); // Instant Big Damage so that enemy will die from a hit
        }
        
        Destroy(gameObject);
    }
}
