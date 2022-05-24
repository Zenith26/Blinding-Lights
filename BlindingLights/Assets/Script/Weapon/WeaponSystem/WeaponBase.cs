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
        public int projectileCount = 1; // amounts of bullet in one shot (shotgun will have more than 5)
        public float anglePerRound = 0.5f; // the angle of each projectileCount

        [HideInInspector]
        public float AdjustedFiringRate = 0; // this will adjust the fire rate for the game
    }

    [SerializeField] WeaponData weaponData = new WeaponData();

    float timeTillCanFire = 0; // check for how long player can shoot again

    [SerializeField] GameObject FirePoint;

    //private AudioSource weaponSound;// the sound for the gun

    //Get Player Aim (Put it here to check if player is still aiming when shoot)
    AimBehaviourBasic aimScript; // Script
    bool isAiming; // Is the player Aiming

    private void Start()
    {
                                    //  60 / firingRate
        weaponData.AdjustedFiringRate = 60 / weaponData.firingRate;

        if (transform.parent.gameObject) // if there is a parent that takes WeaponBase, then set it to Owner
        {
            Owner = transform.parent.gameObject; // Get Player as the Owner
        }

        aimScript = GameManager.Instance.GetPlayer().GetComponent<AimBehaviourBasic>();
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
        isAiming = aimScript.getIsAiming; // Get bool value if player is aiming
        if (isAiming)
        {
            ProjectileLogic();

            timeTillCanFire = Time.time + weaponData.AdjustedFiringRate; // Time.time will not go up if we are not shooting
        }
    }

    protected virtual void ProjectileLogic()
    {
        int numProjectiles = weaponData.projectileCount;

        if (numProjectiles <= 1) // if number projectile is 1 or less
        {
            InstantiateBullet(weaponData.Muzzle.transform.rotation); // needs rotation so we called the rotation of the muzzle
        }
        else
        {
            //is the number of projectiles odd or even
            bool even = numProjectiles % 2 == 0;
            //Calculate the spacing per round based on odd / even number of projectiles
            int adjustedProjNum = even ? numProjectiles / 2 - numProjectiles : -(numProjectiles / 2 - 1) - 1;

            for (int currentRound = 0; currentRound < numProjectiles; currentRound++)
            {
                //takes the muzzle rot to this vector
                Vector3 adjustedRot = weaponData.Muzzle.transform.rotation.eulerAngles;
                int bulletMod = adjustedProjNum + currentRound;

                //muzzle rot += spacing per round * each projectile angle
                adjustedRot.y += bulletMod * weaponData.anglePerRound;

                //additional math if the projectile is even, does not work in the adjusted projectile number line
                if (even)
                {
                    adjustedRot.y += weaponData.anglePerRound / 2;
                }

                InstantiateBullet(Quaternion.Euler(adjustedRot));
            }
        }
    }

    protected virtual void InstantiateBullet(Quaternion rot)
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
