using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomUI : MonoBehaviour
{
    public GameObject BossObj;
    public bool init;
    void Start()
    {
        init = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !init)
        {
            init = true;
            BossObj.GetComponent<BossScript>().hpBar.SetActive(true);
        }
    }
}
