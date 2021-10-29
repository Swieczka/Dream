using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public TextMeshPro textmp;
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
    public int distancetospawn;
    public bool distancechecked;
    public GameObject RoomCamera;
    public DungeonSpawn RoomsStorage;
    public bool ActiveRoom;
    void Start()
    {
        RoomsStorage = GameObject.FindGameObjectWithTag("Spawner").GetComponent<DungeonSpawn>();
    }

    void Update()
    {
        if (ActiveRoom)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && down)
            {
                CameraDown();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && top)
            {
                CameraTop();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && left)
            {
                CameraLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && right)
            {
                CameraRight();
            }
        }
    }

    public void CreateRoom()
    {
        switch (roomType)
        {
            case RoomType.Normal:
                gameObject.name = "NormalRoom";
                Instantiate(RoomTypes[((int)roomType)],transform);
                RoomCamera.SetActive(false);
                ActiveRoom = false;
                break;
            case RoomType.Spawn:
                gameObject.name = "Spawn";
                Instantiate(RoomTypes[((int)roomType)], transform);
                RoomCamera.SetActive(true);
                ActiveRoom = true;
                break;
            case RoomType.Shop:
                gameObject.name = "Shop";
                Instantiate(RoomTypes[((int)roomType)], transform);
                RoomCamera.SetActive(false);
                ActiveRoom = false;
                break;
            case RoomType.Boss:
                gameObject.name = "BossRoom";
                Instantiate(RoomTypes[((int)roomType)], transform);
                RoomCamera.SetActive(false);
                ActiveRoom = false;
                break;
        }
        textmp.text = distancetospawn.ToString("00");
    }

    void CameraLeft()
    {
        RoomCamera.SetActive(false);
        ActiveRoom = false;
        Debug.Log("left");
        RoomsStorage.Rooms[Xpos - 1, Ypos].GetComponent<Room>().ActiveRoom = true;
        RoomsStorage.Rooms[Xpos - 1, Ypos].GetComponent<Room>().RoomCamera.SetActive(true);
    }

    void CameraDown()
    {
        RoomCamera.SetActive(false);
        ActiveRoom = false;
        Debug.Log("down");
        RoomsStorage.Rooms[Xpos, Ypos - 1].GetComponent<Room>().ActiveRoom = true;
        RoomsStorage.Rooms[Xpos, Ypos - 1].GetComponent<Room>().RoomCamera.SetActive(true);
    }

    void CameraTop()
    {
        RoomCamera.SetActive(false);
        ActiveRoom = false;
        Debug.Log("up");
        RoomsStorage.Rooms[Xpos, Ypos + 1].GetComponent<Room>().ActiveRoom = true;
        RoomsStorage.Rooms[Xpos, Ypos + 1].GetComponent<Room>().RoomCamera.SetActive(true);
    }

    void CameraRight()
    {
        RoomCamera.SetActive(false);
        ActiveRoom = false;
        Debug.Log("right");
        RoomsStorage.Rooms[Xpos + 1, Ypos].GetComponent<Room>().ActiveRoom = true;
        RoomsStorage.Rooms[Xpos + 1, Ypos].GetComponent<Room>().RoomCamera.SetActive(true);
    }
}
