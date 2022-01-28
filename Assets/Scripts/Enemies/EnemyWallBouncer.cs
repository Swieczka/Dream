using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWallBouncer : EnemyScript
{
    [SerializeField] private float x_speed;
    [SerializeField] private float y_speed;
    float x_speed_st;
    float y_speed_st;
    [SerializeField] bool GoUp;
    [SerializeField] bool GoRight;
    Rigidbody2D rb2d;
    float TimeCheckX;
    float TimeCheckY;
    
    [SerializeField] float TimeCoolDown = 1f;
    public bool IsGolden;

    void Start()
    {
        x_speed_st = x_speed;
        y_speed_st = y_speed;
        StageBonus = PlayerPrefs.GetInt("BossStage", 1);
        enemyHP += StageBonus * 4;
        enemyDamage += StageBonus * 2;
        if (IsGolden)
        {
            gameObject.GetComponent<Animator>().SetBool("IsGolden", true);
        }
        GoUp = RandomStart();
        GoRight = RandomStart();
        rb2d = GetComponent<Rigidbody2D>();
        TimeCheckX = Time.time;
        TimeCheckY = Time.time;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            Move();
        }
        else
        {
            Stop();
        }

    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject player = collision.collider.gameObject;
            player.GetComponent<PlayerStatus>().PlayerHitted(enemyDamage);
        }
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
    public override void Slowed()
    {
        if (IsSlowed)
        {
            StartCoroutine(slowedaction());
        }
    }

    IEnumerator slowedaction()
    {
        x_speed -= 1;
        y_speed -= 1;
        yield return new WaitForSeconds(5f);
        x_speed += 1;
        y_speed += 1;
        IsSlowed = false;
    }

    void Move()
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

    void Stop()
    {
        rb2d.velocity = new Vector2(0, 0);
    }
}
