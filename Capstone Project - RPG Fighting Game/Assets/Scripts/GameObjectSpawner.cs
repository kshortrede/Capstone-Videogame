/// <summary>
/// Elliot Wurst
/// GameObject Spawner --- Spawner for instantiating GameObjects based on their positions in a node plane
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : ScriptableObject
{
	public NodePlane spawnerNodePlane;
	public GameObject mainCharacter;
	public GameObject enemies;
	public GameObject exitDoor;
	public GameObject[] debris;
	
	public GameObjectSpawner(NodePlane newSpawnerNodePlane, GameObject newMainCharacter, GameObject newEnemies, GameObject newExitDoor, GameObject[] newDebris)
	{
		this.spawnerNodePlane = newSpawnerNodePlane;
		this.mainCharacter = newMainCharacter;
		this.enemies = newEnemies;
		this.exitDoor = newExitDoor;
		this.debris = newDebris;
	}
	
	public NodePlane getSpawnerNodePlane()
	{
		return this.spawnerNodePlane;
	}
	
	public void spawnGameObjects()
	{
		Debug.Log("Begin spawning in game objects on the spawner node plane...");
		
		Node[,] nodesInPlane = this.spawnerNodePlane.getNodesInPlane();
		int[,] validLocations = new int[nodesInPlane.GetLength(0),nodesInPlane.GetLength(1)];
		
		//Spawn the main character into the maze
		spawnMainCharacter(nodesInPlane[0,0].getNodePos());
		validLocations[0,0] = 1;
		
		//Spawn the exit door into the maze
		spawnExitDoor(nodesInPlane[nodesInPlane.GetLength(0) - 1,nodesInPlane.GetLength(1) - 1].getNodePos());
		validLocations[nodesInPlane.GetLength(0) - 1,nodesInPlane.GetLength(1) - 1] = 1;
		
		//Spawn enemies and other objects
		int randomizedGameObject;
		int randomizeRange = 7;
		GameObject gameObject;
		
		for(int i = 0; i < nodesInPlane.GetLength(0); i++)
		{
			for(int j = 0; j < nodesInPlane.GetLength(1); j++)
			{
				randomizedGameObject = Random.Range(0, randomizeRange);
				
				// Determine if a GameObject is already in that position
				if (validLocations[i,j] != 1)
				{
					Vector3 nodePos = nodesInPlane[i,j].getNodePos();
					
					switch (randomizedGameObject)
					{
						case 0:
							Debug.Log("Spawning a zombie into the node plane...");
						
							//Spawn a zombie
							gameObject = Instantiate(enemies.transform.GetChild(0).gameObject, nodePos + randomizeOffset(), randomizeRotation());
							DontDestroyOnLoad(gameObject);
							EnvironmentGenerator.environment.Add(gameObject);
							break;
						case 1:
							Debug.Log("Spawning a dragon infant into the node plane...");
						
							//Spawn a dragon infant
							gameObject = Instantiate(enemies.transform.GetChild(1).gameObject, nodePos + randomizeOffset(), randomizeRotation());
							DontDestroyOnLoad(gameObject);
							EnvironmentGenerator.environment.Add(gameObject);
							break;
						case 2:
							Debug.Log("Spawning a vampire eye into the node plane...");
						
							//Spawn a vampire eye
							gameObject = Instantiate(enemies.transform.GetChild(2).gameObject, nodePos + randomizeOffset(), randomizeRotation());
							DontDestroyOnLoad(gameObject);
							EnvironmentGenerator.environment.Add(gameObject);
							break;
						case 3:
							Debug.Log("Spawning a greed eater into the node plane...");
						
							//Spawn a greed eater
							gameObject = Instantiate(enemies.transform.GetChild(3).gameObject, nodePos + randomizeOffset(), randomizeRotation());
							DontDestroyOnLoad(gameObject);
							EnvironmentGenerator.environment.Add(gameObject);
							break;
						case 4:
						case 5:
							Debug.Log("Spawning debris into the node plane...");
						
							//Spawn debris
							gameObject = Instantiate(debris[Random.Range(0, debris.Length)], nodePos + randomizeOffset(), randomizeRotation());
							Vector3 positionOffset = new Vector3(0, ((float) gameObject.transform.localScale.y / 2), 0);
							gameObject.transform.position += positionOffset;
							
							DontDestroyOnLoad(gameObject);
							EnvironmentGenerator.environment.Add(gameObject);
							break;
					}
					
					// Mark that the current position has been filled
					validLocations[i,j] = 1;
				}
			}
		}
	}
	
	public void spawnMainCharacter(Vector3 nodePos)
	{	
		Debug.Log("Spawning the main character into the node plane...");
		GameObject player;
		
		// Instantiate the Main Character.
		player = Instantiate(mainCharacter, nodePos, mainCharacter.transform.rotation);
		DontDestroyOnLoad(player);
		EnvironmentGenerator.environment.Add(player);
	}
	
	public void spawnExitDoor(Vector3 nodePos)
	{	
		Debug.Log("Spawning the main character into the node plane...");
	
		GameObject door;
		Vector3 positionOffset = new Vector3(0, ((float) exitDoor.transform.localScale.y / 2), 0);
		
		// Instantiate the exit door
		door = Instantiate(exitDoor, nodePos + positionOffset, exitDoor.transform.rotation);
		DontDestroyOnLoad(door);
		EnvironmentGenerator.environment.Add(door);
	}
	
	public Quaternion randomizeRotation()
	{
		int randomizedY = Random.Range(0, 360);
		
		return Quaternion.Euler(new Vector3(0, randomizedY, 0)); 
	}
	
	public Vector3 randomizeOffset()
	{
		float randomizedX = Random.Range(0, 2);
		float randomizedZ = Random.Range(0, 2);
		
		return new Vector3(randomizedX, 0, randomizedZ);
	}
}
