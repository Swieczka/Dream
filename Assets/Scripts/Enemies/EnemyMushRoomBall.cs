using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushRoomBall : MonoBehaviour
{
    public int ballDamage;
    public int direction;
    public int speed;
    public bool ballup;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        switch(direction)
        {
            case 1:
                rb.velocity = Vector2.left * speed;
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case 2:
                rb.velocity = new Vector2(-2, -1).normalized * speed;
                transform.rotation = Quaternion.Euler(0, 0, -45);
                break;
            case 3:
                rb.velocity = Vector2.down * speed;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 4:
                rb.velocity = new Vector2(2, -1).normalized * speed;
                transform.rotation = Quaternion.Euler(0, 0, 45);
                break;
            case 5:
                rb.velocity = Vector2.right * speed;
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 6:
                rb.velocity = new Vector2(2, 1).normalized * speed;
                transform.rotation = Quaternion.Euler(0, 0, 135);
                break;
            case 7:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                rb.velocity = Vector2.up * speed;
                break;
            case 8:
                transform.rotation = Quaternion.Euler(0, 0, -135);
                rb.velocity = new Vector2(-2, 1).normalized * speed;
                break;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject player = collision.collider.gameObject;
            player.GetComponent<PlayerStatus>().PlayerHitted(ballDamage);
            if (ballup)
            {
                player.GetComponent<PlayerStatus>().Slowed();
            }
            Destroy(gameObject);
        }
        if (collision.collider.tag == "Ramka")
        {
            Destroy(gameObject);
        }
    }
}
