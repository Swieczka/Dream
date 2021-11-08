using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public string playerName;

    public float playerMovementSpeed;
    public float playerAttackSpeed;
    public float playerAttackRange;

    public float playerAttack;
    public float playerDefense;

    public float playerLuck;
    public int playerHealthPoints;


    public void LoseHP(int damage)
    {
        playerHealthPoints -= damage;
    }
}
