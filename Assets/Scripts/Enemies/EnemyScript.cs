using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int enemyHP;
    public int enemyDamage;
    [SerializeField] Room RoomObj;
    void Start()
    {
        RoomObj = gameObject.transform.parent.transform.parent.gameObject.GetComponent<Room>();
        RoomObj.AliveEnemiesInRoom += 1;
    }
    void Update()
    {
        if (enemyHP <= 0)
        {
            RoomObj.AliveEnemiesInRoom -= 1;
            if(RoomObj.AliveEnemiesInRoom <=0)
            {
                RoomObj.IsRoomFinished = true;
                Destroy(RoomObj.RoomKey);
            }
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
        enemyHP -= x;
        StartCoroutine(EnemyStatusColor("red", 0.15f));
    }
    IEnumerator EnemyStatusColor(string colorname, float time)
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
}
