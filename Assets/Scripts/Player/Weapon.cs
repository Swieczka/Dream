using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Enemy>() !=null)
        {
            collision.gameObject.GetComponent<Enemy>().LoseHP(2);
        }
    }
}
