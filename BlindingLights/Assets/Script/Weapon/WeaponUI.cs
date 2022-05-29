using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    WeaponComp pWeapon;
    public GameObject[] Weapon;
    

    private void Start()
    {
        pWeapon = GameObject.FindObjectOfType<WeaponComp>();
        Debug.Log(pWeapon);
    }

    private void Update()
    {
        if (pWeapon.storeCurrentWeapon == 0)
        {
            Weapon[0].SetActive(true);
            Weapon[1].SetActive(false);
            Weapon[2].SetActive(false); 
            Debug.Log("Weapon1");
            return;
        }

        else if (pWeapon.storeCurrentWeapon == 1)
        {
            Weapon[0].SetActive(false);
            Weapon[1].SetActive(true);
            Weapon[2].SetActive(false);
            Debug.Log("Weapon2");
            return;
        }

        else if (pWeapon.storeCurrentWeapon == 2)
        {
            Weapon[0].SetActive(false);
            Weapon[1].SetActive(false);
            Weapon[2].SetActive(true);
            Debug.Log("Weapon3");
            return;
        }




    }
}
