using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal_Boss : MonoBehaviour
{
    [SerializeField] Room RoomObj;
    void Start()
    {
        RoomObj = gameObject.transform.parent.parent.gameObject.GetComponent<Room>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && RoomObj.IsRoomFinished)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
