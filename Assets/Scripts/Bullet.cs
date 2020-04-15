using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1.5f;
    public float bulletDamage;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Health>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
