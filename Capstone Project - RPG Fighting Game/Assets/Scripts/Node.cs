/// <summary>
/// Elliot Wurst
/// Node --- Container for storing the position of a node as a 3d vector and any connecting nodes
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : ScriptableObject
{
	public Vector3 nodePos; 
	public List<Node> conNodes; 

	public Node()
	{
		this.nodePos = new Vector3(0, 0, 0);
		this.conNodes = new List<Node>();
	}

	public Node(Vector3 newNodePos, List<Node> newConNodes)
	{
		this.nodePos = newNodePos;
		this.conNodes = newConNodes;
	}

	public Vector3 getNodePos()
	{
		return this.nodePos;
	}

	public List<Node> getConNodes()
	{
		return this.conNodes;
	}

	public GameObject connectNode(Node node)
	{
		// Add the node as a connection
		this.conNodes.Add(node);
		
		// Determine the wall height and offset
		float wallHeight = 8;
		Vector3 wallHeightOffset = new Vector3(0, wallHeight / 2, 0); //Ensures that the wall is not in the ground

		// Calculate GameObject properties
		Vector3 wallPos = calculateMidPoint(node.getNodePos()) + wallHeightOffset;
		Vector3 wallMinimumSize = new Vector3(1, wallHeight, 1);
		Vector3 wallScal = calculateDistance(node.getNodePos()) + wallMinimumSize;
		Vector3 wallRota = new Vector3(0, 0, 0);

		GameObject wallObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		wallObject.name = "Wall";
		wallObject.GetComponent<Renderer>().material = Resources.Load("Materials/Stone2", typeof(Material)) as Material;

		wallObject.transform.position = wallPos;
		wallObject.transform.localScale = wallScal;

		return wallObject;
	}

	public Vector3 calculateMidPoint(Vector3 otherNodePos)
	{
		return (this.nodePos + otherNodePos) / 2;
	}

	public Vector3 calculateDistance(Vector3 otherNodePos)
	{
		Vector3 distance = this.nodePos - otherNodePos;

		if (distance.x < 0)
		{
			distance.x *= -1;
		}

		if (distance.y < 0)
		{
			distance.y *= -1;
		}

		if (distance.z < 0)
		{
			distance.z *= -1;
		}

		return distance;
	}
}
