using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemyHP;
    public int enemyDamage;
    void Start()
    {
        Debug.Log("start");
    }
    void Update()
    {
        Debug.Log(enemyHP);
        if (enemyHP < 0)
        {
            Debug.Log("asd");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject player = collision.collider.gameObject;
            player.GetComponent<PlayerStatus>().PlayerHitted(enemyDamage);
        }
    }
    public void LoseHP(int x)
    {
        Debug.Log("oof");
        enemyHP -= x;
        //  Destroy(gameObject);
    }
}
