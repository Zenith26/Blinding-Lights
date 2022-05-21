using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComp : MonoBehaviour
{
    GameObject Owner = null;

    [SerializeField] List<WeaponBase> InitialWeapons = null; // WE ADD IT ON EDITOR
    [SerializeField] List<WeaponBase> Weapons = null; // Will set the InitialWeapons to here (MAKE SURE TO NOT HAVE ANY INDEX IN THE INSPECTOR, EVEN IF ITS EMPTY IT COUNTS AS INDEX 0.

    WeaponBase CurrentWeapon = null; // the current weapon in the game

    int CurrentWeaponIndex = 0;
    [HideInInspector] public int storeCurrentWeapon; // for GunImage to get the current weapon references, Not using CurrentWeaponIndex since its dangerous

    public GameObject WeaponSpawnPoint; // Set Player Fire Point from the editor

    private void Start()
    {
        Owner = gameObject;

        SpawnWeapon();
        
    }

    void SpawnWeapon()
    {
        //Defining null transform
        Vector3 _loc = Vector3.zero;
        Quaternion _rot = Quaternion.identity;

        //iterating through and spawning weapons in the list
        foreach(WeaponBase gun in InitialWeapons)
        {
            //if there's at least a weapon in the array slot, continue
            if (gun)
            {
                WeaponBase _weapon = Instantiate(gun, _loc, _rot);

                _weapon.Owner = Owner;

                //Attaching weapon to player, which is the gameobject's transform
                _weapon.transform.parent = gameObject.transform;

                _weapon.transform.localPosition = Vector3.zero; // zero means all 0 on pos, identity means all 0 on rot
                _weapon.transform.localRotation = Quaternion.identity;

                //Add our weapon to the (Weapon) list
                Weapons.Add(_weapon);

                //Set the first spawned weapon to be the current weapon
                if (CurrentWeapon == null)
                {
                    CurrentWeapon = _weapon;
                }
            }
        }

        InitialWeapons.Clear(); // once spawned all weapons in the foreach, clears the List
    }

    public void StartFire()
    {
        //if weapon is valid, call weapon with FirePoint references
        CurrentWeapon?.TriggerPulled(WeaponSpawnPoint);
    }

    public void StopFire()
    {
        //if weapon is valid, stop firing
        CurrentWeapon?.TriggerReleased();
    }


    public void SwitchWeapon(bool increase) // with Ternary Operator, true go up, false go down
    {
        //This has to be called before the weapon switch
        StopFire();

        // value = condition ? value type : value false --- Ternary Operator
        CurrentWeaponIndex = increase ? CurrentWeaponIndex += 1 : CurrentWeaponIndex -= 1;

        if(CurrentWeaponIndex > Weapons.Count - 1) // If the CurrentWeaponIndex is higher than the list. Go back to 0 (first index)
        {
            CurrentWeaponIndex = 0;
        }

        if(CurrentWeaponIndex < 0)                 // If the CurrentWeaponIndex is lower than 0. Change it to the last index weapon
        {
            CurrentWeaponIndex = Weapons.Count - 1;
        }

        CurrentWeapon = Weapons[CurrentWeaponIndex];
        storeCurrentWeapon = CurrentWeaponIndex;
    }
}
