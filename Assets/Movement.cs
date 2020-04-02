using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    float movX = 0;
    float movY = 0;
    Vector2 dir;
    public float speed = 1;
    public float jumpForce = 1;
    public bool isGrounded;

    float fallMultiplier = 2.5f;
    float lowJumpMultiplier = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
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
            Debug.Log("1");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            Debug.Log("2");
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
