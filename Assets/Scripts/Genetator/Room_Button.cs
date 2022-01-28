using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Button : MonoBehaviour
{
    [SerializeField] Room RoomObj;
    void Start()
    {
        RoomObj = gameObject.transform.parent.transform.parent.gameObject.GetComponent<Room>();
        if(RoomObj.tag=="BossRoom")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            RoomObj.IsRoomFinished = true;
            Destroy(gameObject);
        }
    }
    
}
