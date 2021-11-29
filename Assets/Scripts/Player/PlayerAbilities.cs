using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] float ability_1_cooldown;
    [SerializeField] float ability_2_cooldown;
    [SerializeField] float ability_3_cooldown;
    [SerializeField] float ability_1_cooldown_def;
    [SerializeField] float ability_2_cooldown_def;
    [SerializeField] float ability_3_cooldown_def;
    void Start()
    {
    }

    void Update()
    {
        if(ability_1_cooldown > 0f)
        {
            ability_1_cooldown -= Time.deltaTime;
        }
        else if(ability_1_cooldown <= 0f)
        {
            ability_1_cooldown = 0f;
        }

        if (ability_2_cooldown > 0f)
        {
            ability_2_cooldown -= Time.deltaTime;
        }
        else if (ability_2_cooldown <= 0f)
        {
            ability_2_cooldown = 0f;
        }

        if (ability_3_cooldown > 0f)
        {
            ability_3_cooldown -= Time.deltaTime;
        }
        else if (ability_3_cooldown <= 0f)
        {
            ability_3_cooldown = 0f;
        }
    }
    public void SetCooldown()
    {
        switch (gameObject.GetComponent<PlayerStats>().playerClass)
        {
            case PlayerStats.PlayerClass.Warrior:
                ability_1_cooldown = 0f;
                ability_1_cooldown_def = 12f;
                ability_2_cooldown = 0f;
                ability_2_cooldown_def = 15f;
                ability_3_cooldown = 0f;
                ability_3_cooldown_def = 7f;
                break;
        }
    }
    public void UseAbility(int ability_index)
    {
        switch (gameObject.GetComponent<PlayerStats>().playerClass)
        {
            case PlayerStats.PlayerClass.Warrior:
                switch (ability_index)
                {
                    case 1:
                        if (ability_1_cooldown == 0f)
                        {
                            StartCoroutine("SwordSpin360");
                            ability_1_cooldown = ability_1_cooldown_def;
                        }
                        break;
                    case 2:
                        if (ability_2_cooldown == 0f)
                        {
                            StartCoroutine("GoldenSkin");
                            ability_2_cooldown = ability_2_cooldown_def;
                        }
                        break;
                    case 3:
                        if(ability_3_cooldown ==0f)
                        {
                            ability_3_cooldown = ability_3_cooldown_def;
                        }
                        break;
                }
                break;
            case PlayerStats.PlayerClass.Archer:
                switch (ability_index)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
            case PlayerStats.PlayerClass.Mage:
                switch (ability_index)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
                break;
        }
    }
    IEnumerator SwordSpin360()
    {
        GameObject sword = GameObject.Find("Sword");
        sword.GetComponent<Animator>().speed = gameObject.GetComponent<PlayerStats>().playerAttackSpeed;
        sword.GetComponent<Animator>().SetBool("Attack360", true);
        sword.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f / gameObject.GetComponent<PlayerStats>().playerAttackSpeed);
        sword.GetComponent<Animator>().SetBool("Attack360", false);
        sword.GetComponent<BoxCollider2D>().enabled = false;
        sword.GetComponent<Animator>().speed = 1;
    }

    IEnumerator GoldenSkin()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0.85f,0.55f,0.25f,1f);
        gameObject.GetComponent<PlayerStats>().playerDefense += 10;
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<PlayerStats>().playerDefense -= 10;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
