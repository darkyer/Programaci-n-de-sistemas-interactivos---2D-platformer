using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    float movX = 0;
    float movY = 0;
    Vector2 dir;
    public float speed = 1;
    public float jumpForce = 1;
    public bool isGrounded;

    public float fallMultiplier = 1.5f;
    public float lowJumpMultiplier = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");




        if (movX < 0)
        {
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        if (movX > 0)
        {
            //transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        animator.SetFloat("Speed", Mathf.Abs(movX));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
            animator.SetBool("isJumping", true);
        }

        BetterJump();

        dir = new Vector2(movX, movY);

        Move(dir);
    }


    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    private void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }
    }

    private void Move(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        
    }
}
