/// <summary>
/// Elliot Wurst
/// Environment Generator --- Generator for creating all physical and non-physical assets that are required for the environment
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
	public GameObject mainCharacter;
	public GameObject enemies;
	public GameObject exitDoor;
	public GameObject[] debris;

	public static List<GameObject> environment = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("Start the environment generation process...");
		
		// Checks to see if the physical environment is stored
		if (environment == null || environment.Count == 0) {
			// Define parameters for the new maze
			int mazeXWidth = 10;
			int mazeZWidth = 10;
			float nodeDistance = 7;
			Vector3 mazeStartNodePos = new Vector3(0, 0, 0);
			
			// Ensure the physical environment is empty
			environment = new List<GameObject>();

			// Generate the maze with the parameters and populate it with game assets
			beginMazeGenerationProcess(mazeStartNodePos, mazeXWidth, mazeZWidth, nodeDistance);
			populateMazeWithAssets(mazeStartNodePos, mazeXWidth, mazeZWidth, nodeDistance);
		}
    }

	public void beginMazeGenerationProcess(Vector3 mazeStartNodePos, int xWidth, int zWidth, float nodeDistance)
	{	
		Debug.Log("Begin the maze generation process...");
	
		// Generate the node plane needed for creating the physical maze
		Node mazeStartNode = new Node(mazeStartNodePos, new List<Node>());

		NodePlane mazeNodePlane = new NodePlane(xWidth, zWidth, nodeDistance);
		mazeNodePlane.generateNodes(mazeStartNode);

		// Begin recursive division process of generating the layout of the maze
		int[,] mazeWallGrid = new int[xWidth - 1, zWidth - 1];
		divide(mazeWallGrid, 0, 0, xWidth - 1, zWidth - 1, chooseOrientation(xWidth, zWidth));

		generatePhysicalMaze(mazeWallGrid, mazeNodePlane);
	}

	public void populateMazeWithAssets(Vector3 mazeStartNodePos, int xWidth, int zWidth, float nodeDistance)
	{
		Debug.Log("Begin the maze population process...");
		
		// Generate the node plane needed for determining the spawner locations based on the maze location.
		Vector3 spawnerStartNodePos = calculateSpawnerStartNodePos(mazeStartNodePos, nodeDistance);
		Node spawnerStartNode = new Node(spawnerStartNodePos, new List<Node>());

		NodePlane spawnerNodePlane = new NodePlane(xWidth - 1, zWidth - 1, nodeDistance);
		spawnerNodePlane.generateNodes(spawnerStartNode);

		GameObjectSpawner mazeGameObjectSpawner = new GameObjectSpawner(spawnerNodePlane, mainCharacter, enemies, exitDoor, debris);
		mazeGameObjectSpawner.spawnGameObjects();
	}

	public static void resetMaze()
	{
		Debug.Log("Begin the maze reset process...");
		
		// Remove all physical objects from the maze
		foreach (GameObject gameObject in environment)
		{
			Destroy(gameObject);
		}

		// Ensure the physical environment is empty
		environment = new List<GameObject>();
	}

	public void divide(int[,] grid, int startX, int startZ, int xWidth, int zWidth, int orientation)
	{	
		// End case for stopping the division process
		if (xWidth < 2 || zWidth < 2)
		{
			return;
		}

		//Determine if the orientation is horizontal.
		bool isHorizontal = orientation == 1 ? true : false;


		//Determine the starting node(x, z) where the wall will be drawn.
		int wallX = startX + (isHorizontal ? 0 : Random.Range(0, xWidth - 2));
		int wallZ = startZ + (isHorizontal ? Random.Range(0, zWidth - 2) : 0);

		//Determine where the passage through the wall will be.
		int passX = wallX + (isHorizontal ? Random.Range(0, xWidth) : 0);
		int passZ = wallZ + (isHorizontal ? 0 : Random.Range(0, zWidth));

		//Determine the direction the wall will be drawn. 1 = true, 0 = false
		int dirX = isHorizontal ? 1 : 0;
		int dirZ = isHorizontal ? 0 : 1;

		//Determine how long the wall will be.
		int length = isHorizontal ? xWidth : zWidth;

		//Determine what direction is perpendicular to the wall.
		int dir = isHorizontal ? 1 : 2; // 1 corresponds to South while 2 corresponds to East

		//Connect the nodes for the wall.
		for (int i = 0; i < length; i++)
		{
			if (wallX != passX || wallZ != passZ)
			{
				grid[wallX, wallZ] = dir;
			}

			wallX += dirX;
			wallZ += dirZ;
		}

		//Determine the new x and y for first subfield
		int newStartX = startX;
		int newStartZ = startZ;

		//Determine the new width and height of the first subfield
		int newXWidth = isHorizontal ? xWidth : wallX - startX + 1;
		int newZWidth = isHorizontal ? wallZ - startZ + 1 : zWidth;

		//Divide the first subfield recursively
		divide(grid, newStartX, newStartZ, newXWidth, newZWidth, chooseOrientation(newXWidth, newZWidth));

		//Determine the new x and y for second subfield
		newStartX = isHorizontal ? startX : wallX + 1;
		newStartZ = isHorizontal ? wallZ + 1 : startZ;

		//Determine the new width and height of the second subfield
		newXWidth = isHorizontal ? xWidth : startX + xWidth - wallX - 1;
		newZWidth = isHorizontal ? startZ + zWidth - wallZ - 1 : zWidth;

		//Divide the second subfield recursively
		divide(grid, newStartX, newStartZ, newXWidth, newZWidth, chooseOrientation(newXWidth, newZWidth));
	}

	public int chooseOrientation(int xWidth, int zWidth)
	{
		// Determine the orientation of the bisection
		if (xWidth < zWidth)
		{
			return 1; //The bisection will be horizontal
		}
		else if (zWidth < xWidth)
		{
			return 2; //The bisection will be vertical
		}
		else
		{
			return Random.Range(1, 2); //The bisection will be selected at random 
		}
	}

	public void generatePhysicalMaze(int[,] grid, NodePlane mazeNodePlane)
	{
		Debug.Log("Begin the physical maze generation process...");
		
		generateEmptyRoom(mazeNodePlane);
		generateFloor(mazeNodePlane);

		Node[,] nodesInPlane = mazeNodePlane.getNodesInPlane();

		for (int i = 0; i < grid.GetLength(0); i++)
		{
			for (int j = 0; j < grid.GetLength(1); j++)
			{
				if (grid[i, j] == 1) //if grid number equals 1, translate the grid position to the node plane horizontally
				{
					//Connect the nodes
					Node nodeOne = nodesInPlane[i + 1, j];
					Node nodeTwo = nodesInPlane[i + 1, j + 1];

					GameObject wall = nodeOne.connectNode(nodeTwo);

					// Add the wall to the environment list
					DontDestroyOnLoad(wall);
					environment.Add(wall);
				} 
				else if (grid[i, j] == 2) //if grid number equals 2, translate the grid position to the node plane vertically
				{
					//Connect the nodes		
					Node nodeOne = nodesInPlane[i, j + 1];
					Node nodeTwo = nodesInPlane[i + 1, j + 1];

					GameObject wall = nodeOne.connectNode(nodeTwo);

					// Add the wall to the environment list
					DontDestroyOnLoad(wall);
					environment.Add(wall);
				}
				//if grid number equals 0, continue
			}
		}
	}

	public void generateEmptyRoom(NodePlane nodePlane)
	{
		Debug.Log("Begin the empty room generation process...");
		
		Node[,] nodesInPlane = nodePlane.getNodesInPlane();

		Node bottomLeftNode = nodesInPlane[0, 0];
		Node topLeftNode = nodesInPlane[nodesInPlane.GetLength(0) - 1, 0];
		Node bottomRightNode = nodesInPlane[0, nodesInPlane.GetLength(1) - 1];
		Node topRightNode = nodesInPlane[nodesInPlane.GetLength(0) - 1, nodesInPlane.GetLength(1) - 1];

		// Create the walls then add them to the environment list
		GameObject wall = topLeftNode.connectNode(topRightNode);

		// Create a new GameObject with all the parameters of the wall GameObject
		DontDestroyOnLoad(wall);
		environment.Add(wall);

		wall = topRightNode.connectNode(bottomRightNode);
		DontDestroyOnLoad(wall);
		environment.Add(wall);

		wall = bottomRightNode.connectNode(bottomLeftNode);
		DontDestroyOnLoad(wall);
		environment.Add(wall);

		wall = bottomLeftNode.connectNode(topLeftNode);
		DontDestroyOnLoad(wall);
		environment.Add(wall);
	}

	public void generateFloor(NodePlane nodePlane) 
	{
		Debug.Log("Begin the floor generation process...");
		
		Node[,] nodesInPlane = nodePlane.getNodesInPlane();

		// Determine the position of the bottom left and top right node
		Node bottomLeftNode = nodesInPlane[0, 0];
		Node topLeftNode = nodesInPlane[nodesInPlane.GetLength(0) - 1, 0];
		Node topRightNode = nodesInPlane[nodesInPlane.GetLength(0) - 1, nodesInPlane.GetLength(1) - 1];

		Vector3 floorOffset = new Vector3(0, (float) -0.5, 0); //Ensure that all objects will start at y = 0

		// Determine the scale for the floor
		Vector3 floorPos = bottomLeftNode.calculateMidPoint(topRightNode.getNodePos()) + floorOffset;
		Vector3 floorMinimumSize = new Vector3(1, 1, 1);
		float floorScalX = bottomLeftNode.calculateDistance(topLeftNode.getNodePos()).x;
		float floorScalZ = topRightNode.calculateDistance(topLeftNode.getNodePos()).z;

		Vector3 floorScal = new Vector3(floorScalX, 0, floorScalZ) + floorMinimumSize;

		GameObject floorObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		floorObject.name = "Floor";
		floorObject.GetComponent<Renderer>().material = Resources.Load("Materials/Stone1", typeof(Material)) as Material;
		floorObject.layer = 6;

		floorObject.transform.position = floorPos;
		floorObject.transform.localScale = floorScal;

		DontDestroyOnLoad(floorObject);
		environment.Add(floorObject);
	}

	public Vector3 calculateSpawnerStartNodePos(Vector3 mazeStartNodePos, float nodeDistance)
	{
		Vector3 nodeOffset = new Vector3(nodeDistance / 2, 0, nodeDistance / 2);

		Vector3 spawnerStartNodePos = mazeStartNodePos + nodeOffset;

		return spawnerStartNodePos;
	}
}
