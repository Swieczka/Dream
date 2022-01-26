using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int enemyHP;
    public int enemyDamage;
    [SerializeField] protected Room RoomObj;

    private void Awake()
    {
        RoomObj = gameObject.transform.parent.transform.parent.gameObject.GetComponent<Room>();
        RoomObj.AliveEnemiesInRoom += 1;
    }
    void Start()
    {
        
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
            RoomObj.AliveEnemiesInRoom -= 1;
        if (RoomObj.AliveEnemiesInRoom <= 0)
        {
            RoomObj.IsRoomFinished = true;
            Destroy(RoomObj.RoomKey);
        }
        Destroy(gameObject);
    }
}
