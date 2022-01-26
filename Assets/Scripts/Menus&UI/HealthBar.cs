using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text healthText;
    public GameObject Player;
    public bool isPlayerAssigned;
    public bool IsPlayer;
    void Start()
    {
       
    }

    void Update()
    {
        if (isPlayerAssigned)
        {
            if (IsPlayer)
            {
                SetHealth(Player.GetComponent<PlayerStats>().playerHealthPoints, Player.GetComponent<PlayerStats>().playerMaxHP);
            }
            else
            {
                SetHealth(Player.GetComponent<BossScript>().enemyHP, Player.GetComponent<BossScript>().enemyMaxHP);
            }
        }
    }

    public void SetHealth(int health, int maxhealth)
    {
        slider.maxValue = maxhealth;
        slider.value = health;
        healthText.text = health.ToString();
    }
}
