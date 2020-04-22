using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates
{
    Idle,
    Following,
    Attacking
}
public class EnemyState : MonoBehaviour
{
    public EnemyStates currentState = EnemyStates.Idle;
    public Transform firePoint;
    public GameObject player;
    public float detectingDistance = 3f;
    public float attackingDistance = 2f;
    public float playerDistance;
    public Rigidbody2D rb;
    public float speed = 1.5f;
    public GameObject enemyBullet;
    public Transform bulletsParent;

    public float shootTime = 1.5f;
    public float currentShootTime = 0f;

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        switch (currentState)
        {
            case EnemyStates.Idle:
                Idle();
                break;
            case EnemyStates.Following:
                Follow();
                break;
            case EnemyStates.Attacking:
                Attack();
                break;
        }
    }

    private void Idle()
    {
        //Debug.Log("Enemy Idle");
        rb.velocity = Vector2.zero;

        CheckPlayerRange();

        currentShootTime = shootTime;
    }

    private void Follow()
    {
        //Debug.Log("Enemy Follow");
        CheckPlayerRange();

        CheckOrientation();
        rb.velocity = transform.right * speed;

    }

    private void Attack()
    {
        //Debug.Log("Enemy Attack");
        rb.velocity = Vector2.zero;

        CheckPlayerRange();

        CheckOrientation();

        if (currentShootTime > shootTime)
        {
            Shoot();
            currentShootTime = 0;
        }
        else
        {
            currentShootTime += Time.deltaTime;
        }
        
    }

    private void Shoot()
    {
        Instantiate(enemyBullet, firePoint.position, firePoint.rotation, bulletsParent);
    }

    private void CheckPlayerRange()
    {
        if (playerDistance < detectingDistance)
        {
            currentState = EnemyStates.Following;
        }

        if (playerDistance < attackingDistance)
        {
            currentState = EnemyStates.Attacking;
        }

        if (playerDistance > attackingDistance)
        {
            currentState = EnemyStates.Following;
        }

        if (playerDistance > detectingDistance)
        {
            currentState = EnemyStates.Idle;
        }
    }

    public void CheckOrientation()
    {
        if (player.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }

        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectingDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackingDistance);
    }

}
