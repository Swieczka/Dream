using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamkaScript : MonoBehaviour
{
    public GameObject RoomObj;
    public Room room;
    void Start()
    {
        RoomObj = gameObject.transform.parent.gameObject;
        room = RoomObj.GetComponent<Room>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            room.IsRoomActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            room.IsRoomActive = false;
        }
    }
}
