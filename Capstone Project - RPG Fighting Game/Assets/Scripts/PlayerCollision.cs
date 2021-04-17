using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Oliver Luo
public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.tag == "Maze Exit")
		{
			Debug.Log("Collided with " + other.gameObject.tag);
			MainMenuController.loadingState = SceneLoaderState.OPENMAINFROMMAZE;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
			EnvironmentGenerator.resetMaze();
            SceneManager.LoadScene("Loading");
		}
		else if(other.gameObject.tag == "Zombie" || other.gameObject.tag == "Dragon Infant" 
			|| other.gameObject.tag == "Vampire Eye" || other.gameObject.tag == "Greed Eater")
		{
			Debug.Log("Collided with " + other.gameObject.tag);
			InstanceEnemyInfo.enemyTypeName = other.gameObject.tag; // this refers to the specific enemy that the player collided with
			Destroy(other.gameObject);
			
			MainMenuController.loadingState = SceneLoaderState.FIGHTFROMMAZE;
			SceneManager.LoadScene("Loading");
		}
    }
}
