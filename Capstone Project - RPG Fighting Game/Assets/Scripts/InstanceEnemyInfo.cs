using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Oliver Luo
public class InstanceEnemyInfo : MonoBehaviour
{
    // holds the index of the enemy the player collided with
    // so that the fight instance knows which enemy to spawn
    public static string enemyTypeName = "Dragon Infant";
    public static int enemyTypeIndex;
}
