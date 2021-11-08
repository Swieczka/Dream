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
    int bossX = 0;
    int bossY = 0;
    int maxdistance = 0;
    void Start()
    {
        SpawnDungeon();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            DestroyDungeon();
            SpawnDungeon();
        }
    }

    void DestroyDungeon()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        foreach (GameObject Room in Rooms)
        {
            Destroy(Room);
        } 
        
        ClearLog();
    }


    void SpawnDungeon()
    {
        #region initstats
        maxdistance = 0;
        int randomX = Random.Range(4, 8);
        int randomY = Random.Range(4, 8);
        int MaxnumberOfRooms = Random.Range((randomX * randomY) / 2 - (randomX+randomY)/2, randomX + randomY+2);
      //  Debug.Log("X: " + randomX + " Y: " + randomY + " Rooms: " + MaxnumberOfRooms); 
        int numberOfRooms = 1;
        Rooms = new GameObject[randomX, randomY];
        #endregion
        //Tworzenie obiektów na siatce
        #region create rooms
        for (int i = 0; i < randomX; i++)
        {
            for (int j = 0; j < randomY; j++)
            {
                GameObject SpawnedRoom = Instantiate(Room, new Vector3(gameObject.transform.position.x + i * 30, gameObject.transform.position.y + j * 20, gameObject.transform.position.z), Quaternion.identity);
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
       // Debug.Log("spawnX: " + spawnX + " spawnY:" + spawnY);
        Rooms[spawnX, spawnY].GetComponent<Room>().roomType = global::Room.RoomType.Spawn;
        Rooms[spawnX, spawnY].GetComponent<Room>().roomActiveness = global::Room.RoomActiveness.Filled;
        Rooms[spawnX, spawnY].GetComponent<Room>().distancetospawn = 0;
        #endregion
        //--------------------------------------------------------

        //Wygenerowanie istniej¹cych pokojów - selekcja s¹siadów i randomowanie nowych pól
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
         //   Debug.Log("ready: " + readyrooms + " random: " + randomnewroom);
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

        //Sprawdzanie s¹siadów
        #region neighbours
        foreach (GameObject room in Rooms)
        {
            //room.GetComponent<Room>().CreateRoom();
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

        //--------------------------------------------
        //Losowanie pokoju bossa - algorytm
        #region bossgen

        DistanceFromSpawnLeft(spawnX,spawnY, Rooms[spawnX,spawnY].GetComponent<Room>().distancestospawn[0]);
        DistanceFromSpawnRight(spawnX, spawnY, Rooms[spawnX, spawnY].GetComponent<Room>().distancestospawn[1]);
        DistanceFromSpawnTop(spawnX, spawnY, Rooms[spawnX, spawnY].GetComponent<Room>().distancestospawn[2]);
        DistanceFromSpawnDown(spawnX, spawnY, Rooms[spawnX, spawnY].GetComponent<Room>().distancestospawn[3]);
        foreach(GameObject room in Rooms)
        {
            if(room.GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
            {
                int lowestdistance=99;
                for(int i =0;i<=3;i++)
                {
                    if(lowestdistance > room.GetComponent<Room>().distancestospawn[i])
                    {
                        lowestdistance = room.GetComponent<Room>().distancestospawn[i];
                    } 
                }
                room.GetComponent<Room>().distancetospawn = lowestdistance;
                if(lowestdistance>maxdistance)
                {
                    maxdistance = lowestdistance;
                    bossX = room.GetComponent<Room>().Xpos;
                    bossY = room.GetComponent<Room>().Ypos;
                }
            }
        }
        Rooms[bossX, bossY].GetComponent<Room>().roomType = global::Room.RoomType.Boss;
        #endregion

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

        //czyszczenie œmieci
        #region garbage
        foreach (GameObject room in Rooms)
        {
            room.GetComponent<Room>().CreateRoom();
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
    }

    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    public void DistanceFromSpawnLeft(int x, int y, int distance)
    {
        if (Rooms[x, y].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
        {
           
            Rooms[x, y].GetComponent<Room>().distancestospawn[0] = distance;
          /*  if (distance > maxdistance)
            {
                maxdistance = distance;
                bossX = x;
                bossY = y;
            } */
            distance += 1;
            
            Rooms[x, y].GetComponent<Room>().distancechecked = true;
            if (Rooms[x, y].GetComponent<Room>().left)
            {
                if (!Rooms[x - 1, y].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnLeft(x - 1, y, distance);
                }
            }
            if (Rooms[x, y].GetComponent<Room>().right)
            {
                if (!Rooms[x + 1, y].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnLeft(x + 1, y, distance);
                }
            }
            if (Rooms[x, y].GetComponent<Room>().top)
            {
                if (!Rooms[x , y+1].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnLeft(x, y + 1, distance);
                }
            }
            if (Rooms[x, y].GetComponent<Room>().down)
            {
                if (!Rooms[x, y-1].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnLeft(x, y - 1, distance);
                }
                
            }

        }
    }
    public void DistanceFromSpawnRight(int x, int y, int distance)
    {
        if (Rooms[x, y].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
        {

            Rooms[x, y].GetComponent<Room>().distancestospawn[1] = distance;
            /*  if (distance > maxdistance)
              {
                  maxdistance = distance;
                  bossX = x;
                  bossY = y;
              } */
            distance += 1;

            Rooms[x, y].GetComponent<Room>().distancechecked = false;
            if (Rooms[x, y].GetComponent<Room>().right)
            {
                if (Rooms[x + 1, y].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnRight(x + 1, y, distance);
                }
            }
            if (Rooms[x, y].GetComponent<Room>().left)
            {
                if (Rooms[x - 1, y].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnRight(x - 1, y, distance);
                }
            }
            
            if (Rooms[x, y].GetComponent<Room>().top)
            {
                if (Rooms[x, y + 1].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnRight(x, y + 1, distance);
                }
            }
            if (Rooms[x, y].GetComponent<Room>().down)
            {
                if (Rooms[x, y - 1].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnRight(x, y - 1, distance);
                }

            }

        }
    }
    public void DistanceFromSpawnTop(int x, int y, int distance)
    {
        if (Rooms[x, y].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
        {

            Rooms[x, y].GetComponent<Room>().distancestospawn[2] = distance;
            /*  if (distance > maxdistance)
              {
                  maxdistance = distance;
                  bossX = x;
                  bossY = y;
              } */
            distance += 1;

            Rooms[x, y].GetComponent<Room>().distancechecked = true;

            if (Rooms[x, y].GetComponent<Room>().top)
            {
                if (!Rooms[x, y + 1].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnTop(x, y + 1, distance);
                }
            } 
            if (Rooms[x, y].GetComponent<Room>().left)
            {
                if (!Rooms[x - 1, y].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnTop(x - 1, y, distance);
                }
            }
            if (Rooms[x, y].GetComponent<Room>().right)
            {
                if (!Rooms[x + 1, y].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnTop(x + 1, y, distance);
                }
            }
            
            if (Rooms[x, y].GetComponent<Room>().down)
            {
                if (!Rooms[x, y - 1].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnTop(x, y - 1, distance);
                }

            }

        }
    }
    public void DistanceFromSpawnDown(int x, int y, int distance)
    {
        if (Rooms[x, y].GetComponent<Room>().roomActiveness == global::Room.RoomActiveness.Filled)
        {

            Rooms[x, y].GetComponent<Room>().distancestospawn[3] = distance;
            /*  if (distance > maxdistance)
              {
                  maxdistance = distance;
                  bossX = x;
                  bossY = y;
              } */
            distance += 1;

            Rooms[x, y].GetComponent<Room>().distancechecked = false;
            if (Rooms[x, y].GetComponent<Room>().down)
            {
                if (Rooms[x, y - 1].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnDown(x, y - 1, distance);
                }

            }
            if (Rooms[x, y].GetComponent<Room>().right)
            {
                if (Rooms[x + 1, y].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnDown(x + 1, y, distance);
                }
            }
            if (Rooms[x, y].GetComponent<Room>().left)
            {
                if (Rooms[x - 1, y].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnDown(x - 1, y, distance);
                }
            }

            if (Rooms[x, y].GetComponent<Room>().top)
            {
                if (Rooms[x, y + 1].GetComponent<Room>().distancechecked)
                {
                    DistanceFromSpawnDown(x, y + 1, distance);
                }
            }
            

        }
    }
}
