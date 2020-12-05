using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Animator animator;
    TrailRenderer trailRenderer;
    
    Rigidbody2D rigidbody2D;
    public float runSpeed;
    public float dashSpeed;
    float currentSpeed;
    public float jumpForce;
    float moveInput;

    bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    float jumpTimeCounter;
    public float jumpTime;
    bool isJumping;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(moveInput * currentSpeed, rigidbody2D.velocity.y);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded == true)
        {
            currentSpeed = runSpeed;
            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rigidbody2D.velocity = Vector2.up * jumpForce;
            }
            
        }

        if (Input.GetButton("Jump") && isJumping ==  true)
        {
            if (jumpTimeCounter > Mathf.Epsilon)
            {
                rigidbody2D.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
                currentSpeed = dashSpeed;
            }
            
        }
        
        else
        {
            isJumping = false;
            currentSpeed = runSpeed;
        }

        AnimatorController();

        FallOffTheLevel();
    }


    public void AnimatorController()
    {
        bool playerHasHorizontalVelocity = Mathf.Abs(rigidbody2D.velocity.x) > .1;
        if(playerHasHorizontalVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1);
        }

        if (isJumping == true && isGrounded != true)
        {
            animator.Play("Bozo-Flutter");
            trailRenderer.enabled = true;
        }

        else
        {
            animator.Play("Bozo-Blink");
            trailRenderer.enabled = false;
        }
    }

    void FallOffTheLevel()
    {
        if (transform.position.y < -9)
        {
            FindObjectOfType<GameOverMenu>().GameOver();
        }
    }
}
