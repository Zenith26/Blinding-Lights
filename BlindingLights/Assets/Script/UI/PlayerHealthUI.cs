using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    PlayerHealth phealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        phealth = GameObject.FindObjectOfType<PlayerHealth>();
        Modifyhealth(phealth.health);
        phealth.OnDamage += DamageHealth;
    }

    void Modifyhealth(float playerhealth)
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < playerhealth; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
    void DamageHealth()
    {
        Modifyhealth(phealth.health);
    }
}
