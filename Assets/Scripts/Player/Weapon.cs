using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject player;
    private void Start()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            int HitDamage = player.GetComponent<PlayerStats>().playerAttack;
            int luck = player.GetComponent<PlayerStats>().playerLuck;
            int roll100 = Random.Range(1, 101);
            if(roll100+luck >=95)
            {
                HitDamage *= 2;
            }
            if(player.GetComponent<PlayerStats>().IsSlowing)
            {
                if (!collision.gameObject.GetComponent<EnemyScript>().IsSlowed)
                {
                    collision.gameObject.GetComponent<EnemyScript>().IsSlowed = true;
                    collision.gameObject.GetComponent<EnemyScript>().Slowed();
                }
            }
            collision.gameObject.GetComponent<EnemyScript>().LoseHP(HitDamage);
        }
    }
}
