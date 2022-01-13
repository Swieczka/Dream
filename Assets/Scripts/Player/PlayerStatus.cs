using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerHitted(int damage)
    {
        int def = gameObject.GetComponent<PlayerStats>().playerDefense;
        gameObject.GetComponent<PlayerStats>().playerHealthPoints -= damage-def*2;
        StartCoroutine(PlayerStatusColor("red",0.15f));
    }

    IEnumerator PlayerStatusColor (string colorname, float time)
    {
        switch(colorname)
        {
            case "red":
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                break;
        }
        yield return new WaitForSecondsRealtime(time);
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
