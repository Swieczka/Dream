using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator PlayerAnimator;
    Rigidbody2D rb2d;
    Vector2 moveDirection;
    void Start()
    {
        PlayerAnimator = gameObject.GetComponent<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    { 
        if(Input.GetAxisRaw("Vertical") >0)
        {
            PlayerAnimator.SetBool("GoUp", true);
            PlayerAnimator.SetBool("GoDown", false);
            PlayerAnimator.SetBool("GoLeft", false);
            PlayerAnimator.SetBool("GoRight", false);
        }
        else if(Input.GetAxisRaw("Vertical") < 0)
        {
            PlayerAnimator.SetBool("GoUp", false);
            PlayerAnimator.SetBool("GoDown", true);
            PlayerAnimator.SetBool("GoLeft", false);
            PlayerAnimator.SetBool("GoRight", false);
        }
        else if(Input.GetAxisRaw("Vertical") == 0)
        {
            PlayerAnimator.SetBool("GoUp", false);
            PlayerAnimator.SetBool("GoDown", false);
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                PlayerAnimator.SetBool("GoRight", true);
                PlayerAnimator.SetBool("GoLeft", false);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                PlayerAnimator.SetBool("GoRight", false);
                PlayerAnimator.SetBool("GoLeft", true);
            }
            else if (Input.GetAxisRaw("Horizontal") == 0)
            {
                PlayerAnimator.SetBool("GoRight", false);
                PlayerAnimator.SetBool("GoLeft", false);
            }
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
    }

    void Move()
    {
        rb2d.velocity = new Vector2(moveDirection.x * 5, moveDirection.y*5);
    }

}