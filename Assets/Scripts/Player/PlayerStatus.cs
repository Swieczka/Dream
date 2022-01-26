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
        int damagedealt = damage - def * 2;
        if(damagedealt<0)
        {
            damagedealt = 0;
        }
        gameObject.GetComponent<PlayerStats>().playerHealthPoints -= damagedealt;
        StartCoroutine(PlayerStatusColor("red",0.15f));
    }
    public void Slowed()
    {
        StartCoroutine(PlayerSlow());
    }
    public IEnumerator PlayerSlow()
    {
        float speed = gameObject.GetComponent<PlayerStats>().playerStartSpeed;
        gameObject.GetComponent<PlayerStats>().playerMovementSpeed *= 0.5f;
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<PlayerStats>().playerMovementSpeed = speed;
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
