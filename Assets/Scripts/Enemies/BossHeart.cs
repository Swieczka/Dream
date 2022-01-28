using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeart : EnemyScript
{
    public bool IsActive;
    public bool IsDead;
    public GameObject Column;
    public GameObject BossObj;
    public Sprite[] ColumnStates = new Sprite[2];
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void Awake()
    {
        BossObj = GameObject.FindGameObjectWithTag("Boss");
        IsDead = false;
        RoomObj = gameObject.transform.parent.transform.parent.gameObject.GetComponent<Room>();
    }

    private void Start()
    { 
        if(IsActive)
        {
            BrokenHeart();
        }
    }
    private void Update()
    {
        if (enemyHP <= 0)
        {
            BrokenHeart();
        }
    }

    void BrokenHeart()
    {
        IsDead = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        Column.GetComponent<SpriteRenderer>().sprite = ColumnStates[1];
        Column.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    public override void LoseHP(int x)
    {
        if (enemyHP > 0)
        {
            if(x>enemyHP)
            {
                x = enemyHP;
            }
            enemyHP -= x;
            BossObj.GetComponent<BossScript>().enemyHP -= x;
            StartCoroutine(EnemyStatusColor("red", 0.15f));
        }
    }
}
