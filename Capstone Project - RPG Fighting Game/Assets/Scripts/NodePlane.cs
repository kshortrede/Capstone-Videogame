/// <summary>
/// Elliot Wurst
/// Node Plane --- Container for storing a plane of multiple nodes, the starting position, and the distance between the nodes
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePlane : ScriptableObject
{
	public int xWidth;
	public int zWidth;
	public Node[,] nodesInPlane;
	public float nodeDistance;
	
	public NodePlane(int newXWidth, int newZWidth, float newNodeDistance)
	{
		this.xWidth = newXWidth;
		this.zWidth = newZWidth;
		this.nodeDistance = newNodeDistance;
	}
	
	public void generateNodes(Node startNode)
	{
		Node[,] nodes = new Node[this.xWidth, this.zWidth];
		Node node;
		
		for(int i = 0; i < xWidth; i++)
		{
			for(int j = 0; j < zWidth; j++)
			{
				Vector3 nodePos = new Vector3(i * nodeDistance, 0, j * nodeDistance) + startNode.getNodePos();
				
				node = new Node(nodePos, new List<Node>());
				nodes[i,j] = node;
			}
		}
		
		this.nodesInPlane = nodes;
	}
	
	public int getXWidth()
	{
		return this.xWidth;
	}
	
	public int getZWidth()
	{
		return this.zWidth;
	}
	
	public Node[,] getNodesInPlane()
	{
		return this.nodesInPlane;
	}
	
	public float getNodeDistance()
	{
		return this.nodeDistance;
	}
}
