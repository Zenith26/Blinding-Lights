using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// create a static class that will be accessed to everything
public static class GameplayStatics
{
    public static void DealDamage(GameObject DamagedObject, float Damage)
    {
        HealthComponent health = DamagedObject.GetComponent<HealthComponent>();
        if (health)
        {
            health.ApplyDamage(Damage);
        }
    }
}
