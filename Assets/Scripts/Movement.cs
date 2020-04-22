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
    public float groundDistance = 1;
    public bool isGrounded;

    public float fallMultiplier = 1.5f;
    public float lowJumpMultiplier = 1f;
    LayerMask layerMask = 0;
    RaycastHit2D hit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, groundDistance);

        if (hit.collider != null)
        {
            isGrounded = true;
            Debug.DrawRay(transform.position, -Vector3.up * groundDistance, Color.yellow);
            animator.SetBool("isJumping", false);
        }
        else
        {
            isGrounded = false;
            Debug.DrawRay(transform.position, -Vector3.up * groundDistance, Color.red);
            animator.SetBool("isJumping", true);
        }

        if (movX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        if (movX > 0)
        {
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

}
