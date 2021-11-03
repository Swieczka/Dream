using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Doors : MonoBehaviour
{
    PlayerMovement playerMovement;
    public int X;
    public int Y;
    public DungeonSpawn RoomsStorage;
    public GameObject RoomObj;
    Room RoomScript;
    public int direction;
    [SerializeField] Sprite[] DoorSprites;
    void Start()
    {
        RoomsStorage = GameObject.FindGameObjectWithTag("Spawner").GetComponent<DungeonSpawn>();
        RoomObj = gameObject.transform.parent.parent.parent.gameObject;
        RoomScript = RoomObj.GetComponent<Room>();
    }

    void Update()
    {
        if(!RoomObj.GetComponent<Room>().IsRoomFinished)
        {
            GetComponent<SpriteRenderer>().sprite = DoorSprites[0];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = DoorSprites[1];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement = collision.GetComponent<PlayerMovement>();
        if(playerMovement!=null && RoomObj.GetComponent<Room>().IsRoomFinished)
        {
            Teleport(X, Y);
        }
    }

    void Teleport(int x, int y)
    {
        playerMovement.gameObject.transform.position += new Vector3(x,y);
        int Xpos = RoomScript.Xpos;
        int Ypos = RoomScript.Ypos;
        switch (direction) // 0-top 1- down 2-left 3-right
        {
            case 0:
                RoomScript.RoomCamera.SetActive(false);
                RoomScript.ActiveRoom = false;
                RoomsStorage.Rooms[Xpos, Ypos+1].GetComponent<Room>().ActiveRoom = true;
                RoomsStorage.Rooms[Xpos, Ypos + 1].GetComponent<Room>().RoomCamera.SetActive(true);
                break;
            case 1:
                RoomScript.RoomCamera.SetActive(false);
                RoomScript.ActiveRoom = false;
                RoomsStorage.Rooms[Xpos, Ypos - 1].GetComponent<Room>().ActiveRoom = true;
                RoomsStorage.Rooms[Xpos, Ypos - 1].GetComponent<Room>().RoomCamera.SetActive(true);
                break;
            case 2:
                RoomScript.RoomCamera.SetActive(false);
                RoomScript.ActiveRoom = false;
                RoomsStorage.Rooms[Xpos - 1, Ypos].GetComponent<Room>().ActiveRoom = true;
                RoomsStorage.Rooms[Xpos - 1, Ypos].GetComponent<Room>().RoomCamera.SetActive(true);
                break;
            case 3:
                RoomScript.RoomCamera.SetActive(false);
                RoomScript.ActiveRoom = false;
                RoomsStorage.Rooms[Xpos + 1, Ypos].GetComponent<Room>().ActiveRoom = true;
                RoomsStorage.Rooms[Xpos + 1, Ypos].GetComponent<Room>().RoomCamera.SetActive(true);
                break;
        }
      
    }
}
 