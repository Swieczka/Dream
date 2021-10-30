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
        if(Input.GetKey(KeyCode.W))
        {
            PlayerAnimator.SetBool("GoUp", true);
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            PlayerAnimator.SetBool("GoUp", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerAnimator.SetBool("GoDown", true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            PlayerAnimator.SetBool("GoDown", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerAnimator.SetBool("GoLeft", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            PlayerAnimator.SetBool("GoLeft", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerAnimator.SetBool("GoRight", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            PlayerAnimator.SetBool("GoRight", false);
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
