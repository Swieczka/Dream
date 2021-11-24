using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public float playerAttackSpeed;
    public float playerAttackRange;

    public float playerAttack;
    public float playerDefense;

    public float playerLuck;
    public int playerHealthPoints;

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
        ChangePlayerClass(PlayerPrefs.GetString("PlayerClass","Default"));
        gameObject.GetComponent<PlayerMovement>().ChangePlayerClass();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        { 
            ChangePlayerClass("Warrior");
            gameObject.GetComponent<PlayerMovement>().ChangePlayerClass();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangePlayerClass("Mage");
            gameObject.GetComponent<PlayerMovement>().ChangePlayerClass();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangePlayerClass("Archer");
            gameObject.GetComponent<PlayerMovement>().ChangePlayerClass();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangePlayerClass("Default");
            gameObject.GetComponent<PlayerMovement>().ChangePlayerClass();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("PlayerClass", "Default");
    }
}
