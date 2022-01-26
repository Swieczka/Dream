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
     void Start()
    {
        directionindex = Random.Range(1,5)*2-1;
        timercooldown = Time.time;
    }

     void Update()
    {
        if (enemyHP <= 0)
        {
            EnemyDeath();
        }
        if (Time.time > timercooldown)
        {
            timercooldown = Time.time + cooldoown;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject shootedball = Instantiate(ball, gameObject.transform);
        shootedball.GetComponent<EnemyMushRoomBall>().direction = directionindex;
        if(enemyUP)
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
}
