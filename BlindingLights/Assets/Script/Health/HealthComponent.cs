using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float health = 3;
    float maxHealth;

    public MulticastNoParams OnDeath;

    private void Awake()
    {
        maxHealth = health;
    }

    // to override, needs virtual
    public virtual void ApplyDamage(float Damage)
    {
        if(Damage <= 0 || health <= 0)
        {
            return; // if there are no damage or health is 0. Return
        }

        health -= Damage;

        //Debug.Log("Amount of " + Damage + " Damage dealth, current health is " + health);

        if(health <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {

    }
}
