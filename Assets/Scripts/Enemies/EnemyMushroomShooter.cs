using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomShooter : EnemyScript
{
    public float cooldoown;
    public float timercooldown;
    public GameObject ball;
    public bool enemyUP;
    public int directionindex; // 1- left 2 - down 3 - right 4 - up 
    public int speedbonus;
     void Start()
    {
        StageBonus = PlayerPrefs.GetInt("BossStage", 1);
        enemyHP += StageBonus * 4;
        enemyDamage += StageBonus * 2;
        directionindex = Random.Range(1,5)*2-1;
        timercooldown = Time.time;
    }

     void Update()
    {
        if (enemyHP <= 0)
        {
            EnemyDeath();
        }
        if (isActive)
        {
            if (Time.time > timercooldown)
            {
                timercooldown = Time.time + cooldoown;
                Shoot();
            }
        }
    }
    public override void Slowed()
    {
        if (IsSlowed)
        {
            StartCoroutine(slowedaction());
        }
    }
    void Shoot()
    {
        GameObject shootedball = Instantiate(ball, gameObject.transform);
        shootedball.GetComponent<EnemyMushRoomBall>().direction = directionindex;
        shootedball.GetComponent<EnemyMushRoomBall>().speed += speedbonus;
        if (enemyUP)
        {
            directionindex += 1;
        }
        else
        {
            directionindex += 2;
        }
        
        if(directionindex >8)
        {
            directionindex = 1;
        }
    }

    IEnumerator slowedaction()
    {
        cooldoown += 1;
        speedbonus -= 2;
        yield return new WaitForSeconds(6f);
        speedbonus += 2;
        cooldoown -= 1;
        IsSlowed = false;
    }
}
