using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthComponent
{
    public static event Action onPlayerDeath;
    protected override void Death()
    {
        GameManager.Instance.Death(); // Remove Camera from player and enable the camera components
        Destroy(gameObject); // After removing camera, we can destroy the gameobject
        base.Death();     
        onPlayerDeath?.Invoke();
    }
}
