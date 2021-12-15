using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int hP;
    void Start()
    {
        hP = 2;
    }

    void Update()
    {
        if(hP<=0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject player = collision.collider.gameObject;
            player.GetComponent<PlayerStatus>().PlayerHitted();
        }
    }
    public void LoseHP(int x)
    {
        hP -= x;
    }
}
