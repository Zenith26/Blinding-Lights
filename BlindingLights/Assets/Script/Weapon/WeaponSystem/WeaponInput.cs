using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInput : MonoBehaviour
{
    WeaponComp weaponComp;

    private void Start()
    {
        weaponComp = GetComponent<WeaponComp>();
    }

    private void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        // WEAPON
        if (weaponComp)
        {
            // FIRE WEAPON
            if (Input.GetMouseButtonDown(0)) // if player press left mouse while aiming
            {
                weaponComp.StartFire();
            }

            if (Input.GetMouseButtonUp(0))
            {
                weaponComp.StopFire();
            }
            // ------------------------------

            // SCROLL WEAPON
                                           // Unity provided
            float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            if(mouseWheel > 0)
            {
                weaponComp.SwitchWeapon(true);
            }

            if(mouseWheel < 0)
            {
                weaponComp.SwitchWeapon(false);
            }
        }
    }
}
