using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemies;
    void Start()
    {
        int index = Random.Range(0, Enemies.Length);
        Instantiate(Enemies[index],transform.position,Quaternion.identity,gameObject.transform.parent.transform);
        Destroy(gameObject);
    }
}
