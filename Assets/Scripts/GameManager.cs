using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int enemiesLeft;

    public void EnemyDied()
    {
        enemiesLeft--;

        if (enemiesLeft == 0)
        {
            Debug.Log("Player Win------------------");
        }
    }

    public void PlayerDied()
    {
        Debug.Log("Player Died------------------");
    }
}
