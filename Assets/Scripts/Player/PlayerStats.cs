using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class PlayerStats : MonoBehaviour
{
    public enum PlayerClass
    {
        Default,
        Warrior,
        Mage,
        Archer
    }
    
    public string playerName;

    public float playerMovementSpeed;
    public float playerStartSpeed;
    public float playerAttackSpeed;
    public float playerAttackRange;

    public int playerAttack;
    public int playerDefense;

    public int playerLuck;
    public int playerHealthPoints;
    public int playerMaxHP;

    public PlayerClass playerClass;

    
    public void LoseHP(int damage)
    {
        playerHealthPoints -= damage;
    }

    public void ChangePlayerClass(string classname)
    {
        switch(classname)
        {
            case "Default":
                playerClass = PlayerClass.Default;
                PlayerPrefs.SetString("PlayerClass","Default");
                break;
            case "Warrior":
                playerClass = PlayerClass.Warrior;
                PlayerPrefs.SetString("PlayerClass", "Warrior");
                break;
            case "Mage":
                playerClass = PlayerClass.Mage;
                PlayerPrefs.SetString("PlayerClass", "Mage");
                break;
            case "Archer":
                playerClass = PlayerClass.Archer;
                PlayerPrefs.SetString("PlayerClass", "Archer");
                break;
        }
    }
    private void Start()
    {
        playerStartSpeed = playerMovementSpeed;
        playerHealthPoints = PlayerPrefs.GetInt("PlayerHP", 100);
        ChangePlayerClass("Warrior");
        ChangeAttackSpeed();
        gameObject.GetComponent<PlayerMovement>().ChangePlayerClass();
    }
    private void Update()
    {
        if (playerHealthPoints <= 0)
        {
            SceneManager.LoadScene("DeathMenu");
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("PlayerClass", "Warrior");
    }

    public void ChangeAttackSpeed()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("AttackSpeed", playerAttackSpeed);
    }
}
