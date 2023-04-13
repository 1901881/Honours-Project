using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    private int currentHealth;
    private int maxHealth = 3;
    GameObject player;

    private void Start()
    {
        healthBar= GetComponent<Image>();
        player = GameObject.FindWithTag("Player");
        maxHealth = player.GetComponent<PlayerMovement>().health;
    }

    private void Update()
    {
        currentHealth = player.GetComponent<PlayerMovement>().health;
        healthBar.fillAmount= (float)currentHealth/maxHealth;
    }
}
