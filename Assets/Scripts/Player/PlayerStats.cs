using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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
    public int playerEXP;

    public float playerMovementSpeedPlus;
    public float playerMovementSpeedMinus;
    public float playerMovementSpeed;
    public float playerAttackSpeed;

    public int playerAttack;
    public int playerDefense;
    public bool IsSlowing;

    public int CooldownReduction;

    public int playerLuck;
    public int playerHealthPoints;
    public int playerMaxHP;

    public PlayerClass playerClass;
    public PlayerItems playerItems;
    public List<PlayerItems.Item> listUnlocked;
    public List<PlayerItems.Item> items;

    public GameObject item1;
    public GameObject item1_text;
    public GameObject item2;
    public GameObject item2_text;
    public GameObject item3;
    public GameObject item3_text;

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
        Object.DontDestroyOnLoad(this.gameObject);
        item1 = GameObject.Find("Item1");
        item1_text = GameObject.Find("Item1_Desc");
        item2 = GameObject.Find("Item2");
        item2_text = GameObject.Find("Item2_Desc");
        item3 = GameObject.Find("Item3");
        item3_text = GameObject.Find("Item3_Desc");
        playerHealthPoints = PlayerPrefs.GetInt("PlayerHP", 100);
       // PlayerPrefs.GetInt("EXP", 0);
        CheckEQ();
        ChangePlayerClass("Warrior");
        ChangeAttackSpeed();
        gameObject.GetComponent<PlayerMovement>().ChangePlayerClass();
        AddEXP(0);
    }
    private void Update()
    {
        if (playerHealthPoints <= 0)
        {
            int exp = playerEXP + PlayerPrefs.GetInt("EXP", 0);
            PlayerPrefs.SetInt("EXP", exp);
            SceneManager.LoadScene("DeathMenu");
            Destroy(GameObject.FindGameObjectWithTag("UI"));
            Destroy(gameObject);
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

    public void CheckEQ()
    {
        foreach (PlayerItems.Item item in playerItems.items)
        {
            if(item.isUnlocked)
            {
                listUnlocked.Add(item);
            }
        }
        if(listUnlocked.Count ==1)
        {
            listUnlocked[0].isActive = true;
        }
        else if(listUnlocked.Count == 2)
        {
            listUnlocked[0].isActive = true;
            listUnlocked[1].isActive = true;
        }
        else if (listUnlocked.Count == 3)
        {
            listUnlocked[0].isActive = true;
            listUnlocked[1].isActive = true;
            listUnlocked[2].isActive = true;
        }
        else if(listUnlocked.Count >3)
        {
            List<int> templist = new List<int>(listUnlocked.Count);
            for(int j=0;j<listUnlocked.Count;j++)
            {
                templist.Add(j);
            }
            int randitem = templist[Random.Range(0, templist.Count)];
        //    Debug.Log(randitem);
            listUnlocked[randitem].isActive = true;
            templist.Remove(randitem);
            randitem = templist[Random.Range(0, templist.Count)];
        //    Debug.Log(randitem);
            listUnlocked[randitem].isActive = true;
            templist.Remove(randitem);
            randitem = templist[Random.Range(0, templist.Count)];
          //  Debug.Log(randitem);
            listUnlocked[randitem].isActive = true;
            templist.Remove(randitem);

        }
        foreach (PlayerItems.Item item in listUnlocked)
        {
            if(item.isActive)
            {
                items.Add(item);
            }
        }
        int i = 0;
        foreach(PlayerItems.Item item in items)
        {
            switch (i)
            {
                case 0:
                    item1.GetComponent<Image>().sprite = items[i].itemImage;
                    item1_text.GetComponent<TextMeshProUGUI>().text = items[i].itemName;
                    break;
                case 1:
                    item2.GetComponent<Image>().sprite = items[i].itemImage;
                    item2_text.GetComponent<TextMeshProUGUI>().text = items[i].itemName;
                    break;
                case 2:
                    item3.GetComponent<Image>().sprite = items[i].itemImage;
                    item3_text.GetComponent<TextMeshProUGUI>().text = items[i].itemName;
                    break;
            }
            i++;
            switch (item.index) // 0 - runa hp, 1 - runa ataku, 2 - runa speed, 3 - runa slow, 4 - runa szalu, 5 - runa cooldown, 6 - runa armor, 7 - worek, 8 - pierscien, 9 - coin
            {
                case 0:
                    playerHealthPoints += 25;
                    playerMaxHP += 25;
                    break;
                case 1:
                    playerAttack += 5;
                    playerAttackSpeed += 0.2f;
                    break;
                case 2:
                    playerMovementSpeed += 1;
                    break;
                case 3:
                    IsSlowing = true;
                    break;
                case 4:
                    playerAttack += 5;
                    playerMovementSpeed += 1;
                    playerHealthPoints -= 15;
                    playerMaxHP -= 15;
                    break;
                case 5:
                    CooldownReduction += 2;
                    break;
                case 6:
                    playerDefense += 2;
                    break;
                case 7:
                    int rand = Random.Range(0, 6);
                    switch(rand)
                    {
                        case 0:
                            playerHealthPoints += 15;
                            playerMaxHP += 15;
                            break;
                        case 1:
                            playerAttack += 5;
                            playerAttackSpeed += 0.1f;
                            break;
                        case 2:
                            playerMovementSpeed += 1;
                            break;
                        case 3:
                            CooldownReduction += 2;
                            break;
                        case 4:
                            playerDefense += 2;
                            break;
                        case 5:
                            playerLuck += 5;
                            break;
                    }
                    break;
                case 8:
                    List<int> randList = new List<int> { 0, 1, 2, 3, 4, 5 };
                    int randring = Random.Range(0, randList.Count);
                   // Debug.Log(randList.Count+" "+ randring);
                    switch (randring)
                    {
                        case 0:
                            playerHealthPoints += 15;
                            playerMaxHP += 15;
                            break;
                        case 1:
                            playerAttack += 5;
                            playerAttackSpeed += 0.1f;
                            break;
                        case 2:
                            playerMovementSpeed += 1;
                            break;
                        case 3:
                            CooldownReduction += 2;
                            break;
                        case 4:
                            playerDefense += 2;
                            break;
                        case 5:
                            playerLuck += 5;
                            break;
                    }
                    randList.RemoveAt(randring);
                    randring = randList[Random.Range(0, randList.Count)];
                  //  Debug.Log(randList.Count + " " + randring);
                    switch (randring)
                    {
                        case 0:
                            playerHealthPoints -= 10;
                            playerMaxHP -= 10;
                            break;
                        case 1:
                            playerAttack -= 3;
                            playerAttackSpeed -= 0.1f;
                            break;
                        case 2:
                            playerMovementSpeed -= 0.5f;
                            break;
                        case 3:
                            CooldownReduction -= 1;
                            break;
                        case 4:
                            playerDefense -= 1;
                            break;
                        case 5:
                            playerLuck -= 4;
                            break;
                    }
                    break;
                case 9:
                    playerLuck += 7;
                        break;
            }
        }


            
    }
    public void AddEXP(int exp)
    {
        playerEXP += exp;
        GameObject expui = GameObject.FindGameObjectWithTag("expUI");
        expui.GetComponent<TMPro.TextMeshProUGUI>().text = "PD: " + playerEXP.ToString();
    }

    public void SaveDataOnLevel()
    {
      //  PlayerPrefs.SetInt("PlayerHP", playerHealthPoints);
      //  int exp = playerEXP + PlayerPrefs.GetInt("EXP", 0);
       // PlayerPrefs.SetInt("EXP", exp);
    }
}
