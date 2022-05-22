using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : HealthComponent
{
    protected override void Death()
    {
        GameManager.Instance.Death(); // Remove Camera from player and enable the camera components
        Destroy(gameObject); // After removing camera, we can destroy the gameobject
        base.Death();

       
    }
}
