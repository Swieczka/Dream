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
    public GameObject[] RoomsNormal;
    public GameObject[] RoomsSpecial;
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
    public int[] distancestospawn = new int[4] { 0, 0, 0, 0 };
    public bool distancechecked;
    public GameObject RoomCamera;
    public bool ActiveRoom;
    public bool IsRoomFinished;
    [SerializeField] GameObject[] DoorPositions; // 0-top 1- down 2-left 3-right
    [SerializeField] GameObject[] DoorToSpawn;
    void Start()
    {
        IsRoomFinished = false;
    }

    void Update()
    {
    }

    public void CreateRoom()
    {
        switch (roomType)
        {
            case RoomType.Normal:
                gameObject.name = "NormalRoom";
                Instantiate(RoomsNormal[Random.Range(0,RoomsNormal.Length)],transform);
                RoomCamera.SetActive(false);
                ActiveRoom = false;
                break;
            case RoomType.Spawn:
                gameObject.name = "Spawn";
                Instantiate(RoomsSpecial[((int)roomType-1)], transform);
                RoomCamera.SetActive(true);
                ActiveRoom = true;
                break;
            case RoomType.Shop:
                gameObject.name = "Shop";
                Instantiate(RoomsSpecial[((int)roomType-1)], transform);
                RoomCamera.SetActive(false);
                ActiveRoom = false;
                break;
            case RoomType.Boss:
                gameObject.name = "BossRoom";
                Instantiate(RoomsSpecial[((int)roomType-1)], transform);
                RoomCamera.SetActive(false);
                ActiveRoom = false;
                break;
        }
        textmp.text = distancetospawn.ToString("00");
        DoorSpawn();
    }
    /*
    void CameraLeft()
    {
        RoomCamera.SetActive(false);
        ActiveRoom = false;
        Debug.Log("left");
        RoomsStorage.Rooms[Xpos - 1, Ypos].GetComponent<Room>().ActiveRoom = true;
        RoomsStorage.Rooms[Xpos - 1, Ypos].GetComponent<Room>().ActiveRoom = true;
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
    */
    void DoorSpawn()
    {
        if(top)
        {
            GameObject Door = Instantiate(DoorToSpawn[0], DoorPositions[0].transform.position, Quaternion.identity);
            Door.transform.parent = DoorPositions[0].transform;
        }
        if (down)
        {
            GameObject Door =  Instantiate(DoorToSpawn[1], DoorPositions[1].transform.position, Quaternion.identity);
            Door.transform.parent = DoorPositions[1].transform;
        }
        if (left)
        {
            GameObject Door =  Instantiate(DoorToSpawn[2], DoorPositions[2].transform.position, Quaternion.identity);
            Door.transform.parent = DoorPositions[2].transform;
        }
        if (right)
        {
            GameObject Door =  Instantiate(DoorToSpawn[3], DoorPositions[3].transform.position, Quaternion.identity);
            Door.transform.parent = DoorPositions[3].transform;
        }
    }
}
