using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1.5f;
    public float autoDestroyTime = 3f;
    public float bulletDamage;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, autoDestroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health collisionHealth = collision.GetComponent<Health>();

        if (collisionHealth)
        {
            collisionHealth.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
