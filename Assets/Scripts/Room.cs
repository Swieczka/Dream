using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum RoomType
    {
        Normal,
        Spawn,
        Shop,
        Boss
    }
    public enum RoomActiveness
    {
        Empty,
        Ready,
        Filled,
    }
    public GameObject[] RoomTypes;
    public RoomType roomType;
    public RoomActiveness roomActiveness;
    public int Xpos;
    public int Ypos;
    public bool left;
    public bool right;
    public bool top;
    public bool down;
    public int neighbournumbers;
    void Start()
    {

    }

    void Update()
    {
        /* switch(roomType)
         {
             case RoomType.Normal:
                 gameObject.name = "NormalRoom";
                 break;
             case RoomType.Spawn:
                 gameObject.name = "Spawn";
                 break;
             case RoomType.Shop:
                 gameObject.name = "Shop";
                 break;
             case RoomType.Boss:
                 gameObject.name = "BossRoom";
                 break;
         } */
    }

    public void CreateRoom()
    {
        switch (roomType)
        {
            case RoomType.Normal:
                gameObject.name = "NormalRoom";
                Instantiate(RoomTypes[((int)roomType)],transform);
                break;
            case RoomType.Spawn:
                gameObject.name = "Spawn";
                Instantiate(RoomTypes[((int)roomType)], transform);
                break;
            case RoomType.Shop:
                gameObject.name = "Shop";
                Instantiate(RoomTypes[((int)roomType)], transform);
                break;
            case RoomType.Boss:
                gameObject.name = "BossRoom";
                Instantiate(RoomTypes[((int)roomType)], transform);
                break;
        }
    }
}
