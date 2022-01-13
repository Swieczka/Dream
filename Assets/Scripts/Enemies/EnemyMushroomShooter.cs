using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroomShooter : MonoBehaviour
{
    public float cooldoown;
    public float timercooldown;
    public GameObject ball;
    public int directionindex; // 1- left 2 - down 3 - right 4 - up
    void Start()
    {
        timercooldown = Time.time;
    }

    void Update()
    {
        if(Time.time > timercooldown)
        {
            timercooldown = Time.time + cooldoown;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject shootedball = Instantiate(ball, gameObject.transform);
        shootedball.GetComponent<EnemyMushRoomBall>().direction = directionindex;
        directionindex++;
        if(directionindex >4)
        {
            directionindex = 1;
        }
    }
}
