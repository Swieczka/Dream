using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    void Start()
    {
        Instantiate(Enemies[0],transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
