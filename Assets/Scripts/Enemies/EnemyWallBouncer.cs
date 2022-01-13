using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallBouncer : MonoBehaviour
{
    [SerializeField] private float x_speed;
    [SerializeField] private float y_speed;
    [SerializeField] bool GoUp;
    [SerializeField] bool GoRight;
    Rigidbody2D rb2d;
    float TimeCheckX;
    float TimeCheckY;
    [SerializeField] float TimeCoolDown = 1f;
    public bool IsGolden;

    void Start()
    {
        if(IsGolden)
        {
            gameObject.GetComponent<Animator>().SetBool("IsGolden", true);
        }
        GoUp = RandomStart();
        GoRight = RandomStart();
        rb2d = GetComponent<Rigidbody2D>();
        TimeCheckX = Time.time;
        TimeCheckY = Time.time;
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (GoUp)
        {
            if (GoRight)
            {
                rb2d.velocity = new Vector2(x_speed, y_speed);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                rb2d.velocity = new Vector2(-x_speed, y_speed);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else
        {
            if (GoRight)
            {
                rb2d.velocity = new Vector2(x_speed, -y_speed);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                rb2d.velocity = new Vector2(-x_speed, -y_speed);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        foreach (ContactPoint2D hitpos in collision.contacts)
            {
                // Debug.Log(hitpos.normal);
                if (hitpos.normal.y < 0 || hitpos.normal.y > 0)
                {
                    SwitchY();
                }
                if (hitpos.normal.x < 0 || hitpos.normal.x > 0)
                {
                    SwitchX();
                }
            }
    }

    bool RandomStart()
    {
        return (Random.value >= 0.5f);
    }

    void SwitchY()
    {
        if (Time.time > TimeCheckY)
        {
            TimeCheckY = Time.time + TimeCoolDown;
            if (!GoUp)
            {
                GoUp = true;
            }
            else
            {
                GoUp = false;
            }
          //  y_speed *= 1.01f;
        }
    }
    void SwitchX()
    {
        if (Time.time > TimeCheckX)
        {
            TimeCheckX = Time.time + TimeCoolDown;
            if (GoRight)
            {
                GoRight = false;
            }
            else
            {
                GoRight = true;
            }
           // x_speed *= 1.01f;
        }
    }
}
