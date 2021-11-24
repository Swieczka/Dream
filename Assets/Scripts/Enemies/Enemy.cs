using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int hP;
    void Start()
    {
        hP = 2;
    }

    void Update()
    {
        if(hP<=0)
        {
            Destroy(gameObject);
        }
    }

    public void LoseHP(int x)
    {
        hP -= x;
    }
}
