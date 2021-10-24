using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class DungeonSpawn : MonoBehaviour 
{
    public GameObject Room;
    public GameObject[,] Rooms;
    int roomsX;
    int roomsY;
    [SerializeField] int neighlimit;
    void Start()
    {
        SpawnDungeon2();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            DestroyDungeon();
            SpawnDungeon2();
        }
    }

    void SpawnDungeon()
    {
        roomsX = Random.Range(2, 6);
        roomsY = Random.Range(2, 6);
        Debug.Log("Wylosowane numery to: X- " + roomsX + "  Y- " + roomsY);
        Rooms = new GameObject[roomsX, roomsY];

        for (int i = 0; i < roomsX; i++)
        {
            for (int j = 0; j < roomsY; j++)
            {
                Debug.Log(i.ToString() + " , " + j.ToString());
                GameObject SpawnedRoom = Instantiate(Room, new Vector3(gameObject.transform.position.x + i * 3, gameObject.transform.position.y + j * 3, gameObject.transform.position.z), Quaternion.identity);
                Rooms[i, j] = SpawnedRoom;
            }
        }
        int randomX = Random.Range(0, roomsX);
        int randomY = Random.Range(0, roomsY);
        Debug.Log("x: " + randomX + "\ny: " + randomY);
     //   Rooms[randomX, randomY].GetComponent<SpriteRenderer>().color = Color.red;
    }

    void DestroyDungeon()
    {
        foreach(GameObject Room in Rooms)
        {
            Destroy(Room);
        }
        ClearLog();
    }


    void SpawnDungeon2()
    {
        #region initstats
        int randomX = Random.Range(4, 8);
        int randomY = Random.Range(4, 8);
        int MaxnumberOfRooms = Random.Range((randomX + randomY) / 2, randomX + randomY);
        Debug.Log("X: " + randomX + " Y: " + randomY + " Rooms: " + MaxnumberOfRooms); 
        int numberOfRooms = 1;
        Rooms = new GameObject[randomX, randomY];
        #endregion
        //Tworzenie obiekt�w na siatce
        #region create rooms
        for (int i = 0; i < randomX; i++)
        {
            for (int j = 0; j < randomY; j++)
            {
                GameObject SpawnedRoom = Instantiate(Room, new Vector3(gameObject.transform.position.x + i * 10, gameObject.transform.position.y + j * 5, gameObject.transform.position.z), Quaternion.identity);
                Rooms[i, j] = SpawnedRoom;
                Rooms[i, j].GetComponent<Room>().Xpos = i;
                Rooms[i, j].GetComponent<Room>().Ypos = j;
            }
        }
        #endregion
        //------------------------------------------------

        //Przypisanie spawnu na odpowiednie kordy
        #region spawn set
        int spawnX = Random.Range(0, randomX);
        int spawnY = Random.Range(0, randomY);
        Debug.Log("spawnX: " + spawnX + " spawnY:" + spawnY);
        Rooms[spawnX, spawnY].GetComponent<Room>().roomType = global::Room.RoomType.Spawn;
        Rooms[spawnX, spawnY].GetComponent<Room>().roomActiveness = global::Room.RoomActiveness.Filled;
        #endregion
        //--------------------------------------------------------

        //Wygenerowanie istniej�cych pokoj�w - selekcja s�siad�w i randomowanie nowych p�l
        #region dungeon generator
        int readyrooms = 0;
        while (numberOfRooms < MaxnumberOfRooms)
        {
            for (int i = 0; i < randomX; i++) 
            {
                for (int j = 0; j < randomY; j++)
                {
                    if (Rooms[i, j].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
                    {
                        for (int a = -1; a <= 1; a += 2)
                        {
                            if (i + a >= 0 && i + a < randomX)
                            {
                                if (Rooms[i + a, j] != null && Rooms[i + a, j].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Empty)
                                {
                                    Rooms[i + a, j].GetComponent<Room>().roomActiveness = global::Room.RoomActiveness.Ready;
                                }
                            }
                            if (j + a >= 0 && j + a < randomY)
                            {
                                if (Rooms[i, j + a] != null && Rooms[i, j + a].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Empty)
                                {
                                    Rooms[i, j + a].GetComponent<Room>().roomActiveness = global::Room.RoomActiveness.Ready;
                                }
                            }
                        }
                    }
                }
            }
            foreach (GameObject room in Rooms)
            {
                if (room.GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Ready)
                {
                    readyrooms += 1;
                }
            }
            int randomnewroom = Random.Range(1, readyrooms);
            Debug.Log("ready: " + readyrooms + " random: " + randomnewroom);
            foreach (GameObject room in Rooms)
            {
                if (room.GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Ready)
                {
                    if (randomnewroom == 1)
                    {
                        int roomX = room.GetComponent<Room>().Xpos;
                        int roomY = room.GetComponent<Room>().Ypos;
                        int neighbours = 0;
                        for(int i=-1;i<=1;i+=1)
                        {
                            for (int j = -1; j <= 1; j += 1)
                            {
                                if (i + roomX >= 0 && i + roomX < randomX && j+roomY >=0 && j + roomY < randomY)
                                {
                                    if (i == 0 && j == 0)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (Rooms[roomX + i, roomY + j].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
                                        {
                                            neighbours++;
                                        }
                                    }
                                }
                            }
                        }
                        if(neighbours <= neighlimit)
                        {
                            room.GetComponent<Room>().roomActiveness = global::Room.RoomActiveness.Filled;
                            numberOfRooms++;
                            
                        }
                       // Debug.Log("X: "+roomX+"  Y:  "+roomY+" neighs: "+neighbours);
                        readyrooms = 0;
                        break;
                    }
                    else
                    {
                        randomnewroom -= 1;
                    }
                }
            }

        }
        #endregion
        //------------------------------------------

        //Losowanie pokoju bossa
        #region bossgen
        int furthestdistance = 0;
        int bossX = 0;
        int bossY = 0;
        foreach(GameObject room in Rooms)
        {
            if (room.GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled && room.GetComponent<Room>().roomType == global::Room.RoomType.Normal)
            {
                int distanceX = ((room.GetComponent<Room>().Xpos - spawnX) * (room.GetComponent<Room>().Xpos - spawnX));
                int distanceY = ((room.GetComponent<Room>().Ypos - spawnY) * (room.GetComponent<Room>().Ypos - spawnY));
                if(distanceX+distanceY > furthestdistance)
                {
                    furthestdistance = distanceX + distanceY;
                    bossX = room.GetComponent<Room>().Xpos;
                    bossY = room.GetComponent<Room>().Ypos;
                }
            }
        }
        Rooms[bossX, bossY].GetComponent<Room>().roomType = global::Room.RoomType.Boss;
        #endregion
        //--------------------------------------------

        //Utworzenie sklepu
        #region shop
        readyrooms = 0;
        foreach (GameObject room in Rooms)
        {
            if (room.GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled && room.GetComponent<Room>().roomType == global::Room.RoomType.Normal)
            {
                readyrooms++;
            }
        }
        int randomshop = Random.Range(1, readyrooms + 1);
        foreach (GameObject room in Rooms)
        {
            if (room.GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled && room.GetComponent<Room>().roomType == global::Room.RoomType.Normal)
            {
                if (randomshop == 1)
                {
                    room.GetComponent<Room>().roomType = global::Room.RoomType.Shop;
                    break;
                }
                else
                {
                    randomshop -= 1;
                }
            }
        }

        #endregion
        //-------------------------------------------

        //czyszczenie �mieci
        #region garbage
        foreach (GameObject room in Rooms)
        {
            if (room.GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Ready)
            {
                room.GetComponent<Room>().roomActiveness = global::Room.RoomActiveness.Empty;
            }
        }
        foreach (GameObject room in Rooms)
        {
            if (room.GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Empty)
            {
              Destroy(room);
            }
        }
        #endregion
        //---------------------------------------------


        //Sprawdzanie s�siad�w
        #region neighbours
        foreach (GameObject room in Rooms)
        {
            room.GetComponent<Room>().CreateRoom();
            if (room.GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
            {
                int x = room.GetComponent<Room>().Xpos;
                int y = room.GetComponent<Room>().Ypos;
                if ((x - 1) >= 0 && Rooms[x - 1, y].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
                {
                    room.GetComponent<Room>().neighbournumbers += 1;
                    room.GetComponent<Room>().left = true;
                }
                if ((x + 1) < randomX && Rooms[x + 1, y].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
                {
                    room.GetComponent<Room>().neighbournumbers += 1;
                    room.GetComponent<Room>().right = true;
                }
                if ((y - 1) >= 0 && Rooms[x, y - 1].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
                {
                    room.GetComponent<Room>().neighbournumbers += 1;
                    room.GetComponent<Room>().down = true;
                }
                if ((y + 1) < randomY && Rooms[x, y + 1].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
                {
                    room.GetComponent<Room>().neighbournumbers += 1;
                    room.GetComponent<Room>().top = true;
                }
            }
        }
        #endregion
        //----------------------------------------------------
       // Instantiate(Hero, Rooms[spawnX, spawnY].transform);
    }

    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
