using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthComponent
{
    protected override void Death()
    {
        Destroy(gameObject);

        GameManager.Instance.Death(); // Call Death
        base.Death();
    }
}
