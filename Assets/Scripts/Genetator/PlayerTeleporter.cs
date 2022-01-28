using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] GameObject Player;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = new Vector3(gameObject.transform.position.x+12.5f, gameObject.transform.position.y-9f, gameObject.transform.position.z); 
            //gameObject.transform.position;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
