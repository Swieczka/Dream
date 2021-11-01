using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject Player;
    void Start()
    {
        GameObject PlayerSpawned = Instantiate(Player, transform.position, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
