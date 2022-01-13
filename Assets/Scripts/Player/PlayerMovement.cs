using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    Animator PlayerAnimator;
    [Serializable]
    public class Animators
    {
        [Header("Animators")]
         public AnimatorOverrideController DefaultAnimator;
         public AnimatorOverrideController WarriorAnimator;
         public AnimatorOverrideController ArcherAnimator;
         public AnimatorOverrideController MageAnimator;
    }

    [SerializeField] Animators animators;
    Rigidbody2D rb2d;
    Vector2 moveDirection;
    PlayerStats playerStats;
    void Start()
    {
        GameObject hpBar = GameObject.FindGameObjectWithTag("HealthBar");
        hpBar.GetComponent<HealthBar>().Player = gameObject;
        hpBar.GetComponent<HealthBar>().isPlayerAssigned = true;
        playerStats = GetComponent<PlayerStats>();
        PlayerAnimator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameObject.GetComponent<PlayerAbilities>().UseAbility(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameObject.GetComponent<PlayerAbilities>().UseAbility(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gameObject.GetComponent<PlayerAbilities>().UseAbility(3);
        }

        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        PlayerAnimator.SetFloat("Horizontal", moveX);
        PlayerAnimator.SetFloat("Vertical", moveY);

        float attackX = Input.GetAxisRaw("HorizontalAttack");
        float attackY = Input.GetAxisRaw("VerticalAttack");
        
        if(attackX != 0 || attackY != 0)
        {
            PlayerAnimator.SetTrigger("AttackTrigger");
            PlayerAnimator.SetFloat("HorizontalAttack", attackX);
            PlayerAnimator.SetFloat("VerticalAttack", attackY);
        }
        else
        {
            PlayerAnimator.ResetTrigger("AttackTrigger");
        }
        
    }

    void Move()
    {
        rb2d.velocity = new Vector2(moveDirection.x * playerStats.playerMovementSpeed, moveDirection.y * playerStats.playerMovementSpeed);
    }

    public void ChangePlayerClass()
    {
        switch (gameObject.GetComponent<PlayerStats>().playerClass)
        {
            case PlayerStats.PlayerClass.Default:
                PlayerAnimator.runtimeAnimatorController = animators.DefaultAnimator;
                break;
            case PlayerStats.PlayerClass.Warrior:
                PlayerAnimator.runtimeAnimatorController = animators.WarriorAnimator;
                break;
            case PlayerStats.PlayerClass.Mage:
                PlayerAnimator.runtimeAnimatorController = animators.MageAnimator;
                break;
            case PlayerStats.PlayerClass.Archer:
                PlayerAnimator.runtimeAnimatorController = animators.ArcherAnimator;
                break;
        }
        gameObject.GetComponent<PlayerAbilities>().SetCooldown();
    }
} 