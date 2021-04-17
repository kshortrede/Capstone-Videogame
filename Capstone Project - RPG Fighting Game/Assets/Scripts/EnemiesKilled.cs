/// <summary>
/// Kenneth Shortrede
/// Mission System --- Track the amount of enemies killed in each instance of Maze to add it to the necessary mission goals
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use a dictionary with the type of enemy and the amount killed
public class EnemiesKilled : MonoBehaviour
{
    public static Dictionary<EnemyType, int> enemiesKilled = new Dictionary<EnemyType, int>();

    public static void AddEnemyKill(EnemyType type)
    {
        if (enemiesKilled.ContainsKey(type))
        {
            enemiesKilled[type] = enemiesKilled[type] + 1;
        } else
        {
            enemiesKilled.Add(type, 1);
        }        
		
		Debug.Log("Enemy of type " + type + " " + enemiesKilled[type]);
    }
}
