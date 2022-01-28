using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int enemyHP;
    public int enemyDamage;
    public int enemyEXP;
    public bool isActive;
    public int StageBonus;
    public bool IsSlowed;
    public GameObject player;
    [SerializeField] protected Room RoomObj;

    private void Awake()
    {
        RoomObj = gameObject.transform.parent.transform.parent.gameObject.GetComponent<Room>();
        RoomObj.AliveEnemiesInRoom += 1;
    }
    void Start()
    {
        StageBonus = PlayerPrefs.GetInt("BossStage", 1);
        enemyHP += StageBonus * 4;
        enemyDamage += StageBonus * 2;
    }
    private void LateUpdate()
    {
        if(RoomObj.ActiveRoom)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }
    void Update()
    {
        if (enemyHP <= 0)
        {
            EnemyDeath();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            GameObject player = collision.collider.gameObject;
            player.GetComponent<PlayerStatus>().PlayerHitted(enemyDamage);
        }
    }
    public virtual void LoseHP(int x)
    {
        enemyHP -= x;
        StartCoroutine(EnemyStatusColor("red", 0.15f));
    }
    public virtual void Slowed()
    {

    }
    protected IEnumerator EnemyStatusColor(string colorname, float time)
    {
        switch (colorname)
        {
            case "red":
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
        }
        yield return new WaitForSecondsRealtime(time);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    protected void EnemyDeath()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerStats>().AddEXP(enemyEXP);
            RoomObj.AliveEnemiesInRoom -= 1;
        if (RoomObj.AliveEnemiesInRoom <= 0)
        {
            RoomObj.IsRoomFinished = true;
            Destroy(RoomObj.RoomKey);
        }
        Destroy(gameObject);
    }
}
