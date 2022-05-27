using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickup : MonoBehaviour
{
    PlayerHealth phealth;
    public float healthbonus = 1;


    private void Start()
    {
        phealth = GameObject.FindObjectOfType<PlayerHealth>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(phealth.health < 5)
        {
            Destroy(gameObject);
            phealth.health = phealth.health + healthbonus;
        }
    }
}
