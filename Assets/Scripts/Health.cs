using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfCharacter
{
    Enemy,
    Player
}

public class Health : MonoBehaviour
{
    public float health = 100f;
    public TypeOfCharacter type;
    public GameManager gameManager;
    public void TakeDamage(float _damage)
    {
        health -= _damage;

        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        if(type == TypeOfCharacter.Enemy)
        {
            gameManager.EnemyDied();
        }
        else
        {
            gameManager.PlayerDied();
        }
    }
}
