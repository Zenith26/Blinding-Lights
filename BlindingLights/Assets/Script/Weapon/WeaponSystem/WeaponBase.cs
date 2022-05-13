using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [HideInInspector] // opposite of SerializedField
    public GameObject Owner;

    //setting up class for weapon data
    [System.Serializable] // SerializeField for class, struct, enum
    public class WeaponData
    {
        public Transform Muzzle = null; // the muzzle (Gun) from the WeaponBase
        public float firingRate = 500; // rate of fires per shot
        public ProjectileBase Projectile; // the projectile (bullet)

        [HideInInspector]
        public float AdjustedFiringRate = 0; // this will adjust the fire rate for the game
    }

    [SerializeField] WeaponData weaponData = new WeaponData();

    float timeTillCanFire = 0; // check for how long player can shoot again

    [SerializeField] GameObject FirePoint; 

    //private AudioSource weaponSound;// the sound for the gun

    private void Start()
    {
                                    //  60 / firingRate
        weaponData.AdjustedFiringRate = 60 / weaponData.firingRate;

        if (transform.parent.gameObject) // if there is a parent that takes WeaponBase, then set it to Owner
        {
            Owner = transform.parent.gameObject; // Get Player as the Owner
        }

    }

    public void TriggerPulled(GameObject _weaponSpawnPoint)
    {
        // checking to make sure we can fire
        if (!CanFire())
        {
            return;
        }

        //Invoke our firing at auto fire
        InvokeRepeating("FireWeapon", 0, weaponData.AdjustedFiringRate);
        FirePoint = _weaponSpawnPoint; // Set the FirePoint from the func params
    }

    public void TriggerReleased()
    {
        CancelInvoke("FireWeapon");
    }

    // accessible but can't be overriden
    protected bool CanFire()
    {
        //time: gametime since creation, deltatime: time since last frame
        if(Time.time < timeTillCanFire) //ex: 30 < (30 + 60/600)
        {
            return false;
        }
        return true;
    }

    protected virtual void FireWeapon()
    {
        ProjectileLogic();

        timeTillCanFire = Time.time + weaponData.AdjustedFiringRate; // Time.time will not go up if we are not shooting
    }

    protected virtual void ProjectileLogic() // TODO: USE THE MUZZLE ROTATION TO SHOOT AT RETICLE HERE
    {
        InstantiateBullet(weaponData.Muzzle.transform.rotation); // needs rotation so we called the rotation of the muzzle
    }

    protected virtual void InstantiateBullet(Quaternion rot) // TODO: THIS IS FOR THE RAYCAST
    {
        if(weaponData.Projectile == null)
        {
            return;
        }
        //got it from ProjectileLogic()                           // with Player FirePoint location
        ProjectileBase Bullet = Instantiate(weaponData.Projectile, FirePoint.transform.position, rot);

        Bullet.Owner = Owner;
    }
}
