using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDirection
{
    Left,
    Right
}
public class EnemySin : MonoBehaviour
{
    public float speed = 1.5f;
    public EnemyDirection dir;
    public float damage = 50f;
    public Vector2 horizontalLimits;

    [Space(20)]
    [Header("Sin Movement Parameters")]
    public float frequency = 3;
    public float magnitude = 3;
    public Vector3 pos;

    private void Update()
    {
        if(transform.position.x < horizontalLimits.x)
        {
            SwapDir();
        }

        if(transform.position.x > horizontalLimits.y)
        {
            SwapDir();
        }

        switch (dir)
        {
            case EnemyDirection.Left:
                pos += -transform.right * Time.deltaTime * speed;
                transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
                break;
            case EnemyDirection.Right:
                pos += transform.right * Time.deltaTime * speed;
                transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
                break;
        }
    }

    private void SwapDir()
    {
        if(dir == EnemyDirection.Left)
        {
            dir = EnemyDirection.Right;
            transform.position = new Vector3(horizontalLimits.x + 0.1f, transform.position.y, transform.position.z);
        }
        else
        {
            dir = EnemyDirection.Left;
            transform.position = new Vector3(horizontalLimits.y - 0.1f, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health colHealth = collision.GetComponent<Health>();
        if(colHealth && collision.CompareTag("Player"))
        {
            //Debug.Log("Player taking damage");
            colHealth.TakeDamage(damage);
        }
    }
}
