using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFindingScript : MonoBehaviour {
	//this Script is to find the node path that most quickly finds the player.
	private List<NodeScript> openList;//a list of nodes that we have to check
	private List<NodeScript> closedList;//a list of nodes that have been checked and shown to not lead to the player.
	private List<Transform> finalPath;//the final path that we will return at the end of this program.
	public float distanceFromPlayer;

	// Use this for initialization
	void Start () {
		openList = new List<NodeScript>();
		closedList = new List<NodeScript>();
		finalPath = new List<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//we are going through the entire open list, and judging by their node values,
	//determining which node out of all of them is the best node to check next.
	public NodeScript ReturnNextNodeToCheck(Transform goal){
		//if there aren't any more nodes to check
		if(openList.Count < 1){
			return null;
		}
		else{
			NodeScript nextNode = openList[0];
			float nextNodeValue = 1000;
			foreach(NodeScript node in openList){
				if(CalculateNodeData(node, goal) < nextNodeValue){///4. Then, this is the next problem. This is the first time calling this function.
					nextNodeValue = CalculateNodeData(node, goal);
					nextNode = node;
				}
			}
			return nextNode;
		}
	}
	
	//this is to check if the node is the one close to the player
	public List<Transform> FindMap(NodeScript node, Transform goal){///2.Then this function gets called
		closedList.Add(node);
		if(openList.Contains(node)){openList.Remove(node);}
		//if this is the node closest to the player
		if(Vector2.Distance(node.gameObject.GetComponent<Transform>().position, goal.position) < distanceFromPlayer){
			//time to draw the path to the Player!
			return DrawMap(node);
		}
		//if not,
		else{
			//add all of the nodes connected to it that are not on the closed list to the open list
			//also, set this node as the parent of all the nodes addd to the open list.
			foreach(NodeScript nodes in node.ReturnNeighbors()){
				if(!closedList.Contains(nodes)){
					nodes.SetParent(node);
					openList.Add(nodes);
				}
			}
			//then, we will calculate the next node to check, and repeat the cycle
			return FindMap(ReturnNextNodeToCheck(goal), goal);///3. Then it calls itself again. The open list has two nodes to check.
		}
	}
	
	public List<Transform> DrawMap(NodeScript endNode){
		List<Transform> finalPath = new List<Transform>();
		NodeScript parentNode = endNode.ReturnParent();
		//now, we start tracing our path, starting with the end node,
		finalPath.Add(endNode.gameObject.GetComponent<Transform>());
		//now, so long as there is a parent node to add to the list, we add it until we have found the starting node.
		while(parentNode != null){
			finalPath.Add(parentNode.gameObject.GetComponent<Transform>());
			parentNode = parentNode.ReturnParent();
		}
		//now, we clean up the parents of all the nodes, then clear the open and closed lists
		foreach(NodeScript node in openList){
			node.SetParent(null);
		}
		foreach(NodeScript node in closedList){
			node.SetParent(null);
		}
		openList.Clear();
		closedList.Clear();
		//now, we return the final path.
		return finalPath;
	}
	//We'll try putting this here, so that we can get this to work
	public float CalculateNodeData(NodeScript node, Transform goal){
		float nodeWorth = 0;
		nodeWorth += Vector3.Distance(node.gameObject.GetComponent<Transform>().position, goal.position);//distance from player
		
		if(node.ReturnParent() != null){
			nodeWorth += Vector3.Distance(node.gameObject.GetComponent<Transform>().position, node.ReturnParent().gameObject.GetComponent<Transform>().position) + node.ReturnParent().ReturnDistanceFromParent();//to calculate the total distance travelled if we took this path so far.
		}
		return nodeWorth;
	}
	
}
