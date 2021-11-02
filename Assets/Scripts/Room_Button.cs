using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Button : MonoBehaviour
{
    [SerializeField] Room RoomObj;
    void Start()
    {
        // gameObject.transform.position = Math.AddVectors2(gameObject.transform.position, Random.Range(-5f, 5f), Random.Range(-3f, 3f));
        Math.AddVectors2(gameObject, Random.Range(-5f, 5f), Random.Range(-3f, 3f));
        RoomObj = gameObject.transform.parent.gameObject.GetComponent<Room>();
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
