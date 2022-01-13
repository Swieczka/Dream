using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushRoomBall : MonoBehaviour
{
    public int ballDamage;
    public int direction;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        switch(direction)
        {
            case 1:
                gameObject.transform.position += Vector3.left * 0.1f;
                break;
            case 2:
                gameObject.transform.position += Vector3.down * 0.1f;
                break;
            case 3:
                gameObject.transform.position += Vector3.left * -0.1f;
                break;
            case 4:
                gameObject.transform.position += Vector3.down * -0.1f;
                break;

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject player = collision.collider.gameObject;
            player.GetComponent<PlayerStatus>().PlayerHitted(ballDamage);
            Destroy(gameObject);
        }
        if (collision.collider.tag == "Ramka")
        {
            Destroy(gameObject);
        }
    }
}
