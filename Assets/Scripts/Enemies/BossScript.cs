using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyScript
{
    public int StageIndex;
    public int actionIndex;
    public int maxActionIndex;
    public int powerShotIndex;
    public GameObject portal;
    public GameObject ShootingBall;
    public GameObject hpBar;
    public GameObject[] Hearts = new GameObject[4];
    public List<GameObject> HeartsList;
    public int enemyMaxHP;
    public GameObject[] Enemies;
    public float cooldoown;
    public float timercooldown;
    public Animator anim;

    private void Start()
    {
        powerShotIndex = Random.Range(3, 6);
        hpBar = GameObject.FindGameObjectWithTag("BossHPBar");
        hpBar.GetComponent<HealthBar>().Player = gameObject;
        hpBar.GetComponent<HealthBar>().isPlayerAssigned = true;
        hpBar.SetActive(false);
        anim = GetComponent<Animator>();
        timercooldown = Time.time;
        actionIndex = 1;
        enemyHP = 0;
        portal.SetActive(false);
        StageIndex = PlayerPrefs.GetInt("BossStage", 1);
        switch (StageIndex)
        {
            case 0:
                enemyMaxHP = 100;
                enemyHP = 100;
                hpBar.SetActive(false);
                break;
            case 1:
                maxActionIndex = 5;
                break;
            case 2:
                maxActionIndex = 6;
                break;
            case 3:
                maxActionIndex = 5;
                break;
            case 4:
                maxActionIndex = 6;
                break;
        }
        foreach(GameObject heart in Hearts)
        {
            if(heart.activeSelf)
            {
                enemyHP += heart.GetComponent<BossHeart>().enemyHP;
                heart.SetActive(false);
            }
        }
        enemyMaxHP = enemyHP;
    }

    private void Update()
    {
        if (StageIndex != 0)
        {
            BossActions();
        }
        if (enemyHP <= 0)
        {
            BossDeath();
        }
    }

    void BossDeath()
    {
        portal.SetActive(true);
        RoomObj.AliveEnemiesInRoom -= 1;
        if (RoomObj.AliveEnemiesInRoom <= 0)
        {
            RoomObj.IsRoomFinished = true;
            Destroy(RoomObj.RoomKey);
        }
        hpBar.SetActive(false);
        gameObject.SetActive(false);
        
    }

    void BossActions()
    {
        if (Time.time > timercooldown)
        {
            
            switch (StageIndex)
            {
                case 1:
                switch (actionIndex)
                    {
                    case 1: case 3:
                        Shoot(1, 2);
                            timercooldown = Time.time + cooldoown;
                            break;
                    case 2: case 4:
                        Shoot(2, 2);
                            timercooldown = Time.time + cooldoown;
                            break;
                     case 5:
                            StartCoroutine(VulnerableTime(4));
                            timercooldown = Time.time + 4;
                            break;
                    }
                break;
                case 2:
                    switch (actionIndex)
                    {
                        case 1:
                        case 3:
                            Shoot(1, 2);
                            timercooldown = Time.time + cooldoown*0.9f;
                            break;
                        case 2:
                        case 5:
                            Shoot(2, 2);
                            timercooldown = Time.time + cooldoown*0.9f;
                            break;
                        case 4:
                            SpawnMinions();
                            timercooldown = Time.time + cooldoown * 0.9f;
                            break;
                        case 6:
                            StartCoroutine(VulnerableTime(3));
                            timercooldown = Time.time + 3;
                            break;
                    }
                    break;
                case 3:
                    switch (actionIndex)
                    {
                        case 1:
                        case 3:
                            Shoot(1, 1);
                            timercooldown = Time.time + cooldoown * 0.8f;
                            break;
                        case 2:
                        case 4:
                            SpawnMinions();
                            timercooldown = Time.time + cooldoown * 0.8f;
                            break;
                        case 5:
                            StartCoroutine(VulnerableTime(3));
                            timercooldown = Time.time + 3;
                            break;
                    }
                    break;
                case 4:
                    switch (actionIndex)
                    {
                        case 1:
                        case 5:
                            Shoot(1, 1);
                            timercooldown = Time.time + cooldoown * 0.7f;
                            break;
                        case 2:
                        case 4:
                            SpawnMinions();
                            timercooldown = Time.time + cooldoown * 0.7f;
                            break;
                        case 3:
                            if (powerShotIndex == 0)
                            {
                                PowerShoot(1, 1);
                                powerShotIndex = Random.Range(3, 6);
                            }
                            else
                            {
                                powerShotIndex--;
                            }
                            timercooldown = Time.time + 0.1f;
                            break;
                        case 6:
                            StartCoroutine(VulnerableTime(3));
                            timercooldown = Time.time + 3;
                            break;
                    }
                    break;
            }
            actionIndex++;
            if(actionIndex>maxActionIndex)
            {
                actionIndex = 1;
            }

        }
    }

    void Shoot(int directionindex, int nextcounter)
    {
        anim.SetTrigger("Attack");
        for(int i=directionindex;i<=8;i+=nextcounter)
        {
            GameObject shootedball = Instantiate(ShootingBall, gameObject.transform);
            shootedball.GetComponent<EnemyMushRoomBall>().direction = i;
        }
        anim.ResetTrigger("Attack");
    }

    void PowerShoot(int directionindex, int nextcounter)
    {
        anim.SetTrigger("Attack");
        for (int i = directionindex; i <= 8; i += nextcounter)
        {
            GameObject shootedball = Instantiate(ShootingBall, gameObject.transform);
            shootedball.GetComponent<EnemyMushRoomBall>().direction = i;
            shootedball.GetComponent<EnemyMushRoomBall>().ballDamage = 10000;
        }
        anim.ResetTrigger("Attack");
    }

    IEnumerator VulnerableTime(float time)
    {
        foreach (GameObject heart in Hearts)
        {
            if(!heart.GetComponent<BossHeart>().IsDead)
            {
                HeartsList.Add(heart);
            }
        }
        int random = Random.Range(0, HeartsList.Count);
        HeartsList[random].SetActive(true);
        HeartsList[random].GetComponent<Renderer>().material.color = Color.white;
        anim.SetBool("Vulnerable", true);
        yield return new WaitForSeconds(time);
        anim.SetBool("Vulnerable", false);
        HeartsList[random].SetActive(false);
        HeartsList.Clear();
    }
    void SpawnMinions()
    {
        int index = Random.Range(0, Enemies.Length);
        int random = Random.Range(0, 2);
        int randomspawn;
        if(random ==0)
        {
            randomspawn = 5;
        }
        else
        {
            randomspawn = -5;
        }
        Vector3 spawnpos = new Vector3(randomspawn, 0, 0);
        Instantiate(Enemies[index], transform.position+spawnpos, Quaternion.identity, gameObject.transform.parent.transform);
    }
}
