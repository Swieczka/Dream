using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastLevel : MonoBehaviour
{
    public Room room;
    void Start()
    {
        room.CreateRoom();
    }

    void Update()
    {
        
    }
}
