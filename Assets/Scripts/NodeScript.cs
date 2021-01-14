using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//the node script needs to have an array of the other Nodes around it,
//it then needs to be able to tell if the Player is near a certain node.

public class NodeScript : MonoBehaviour {
	private Transform transform;
	public List<NodeScript> neighborNodes = new List<NodeScript>();
	private NodeScript parentNode;//so that this Node, when searching for the player, knows which node connects
//back to the player
	private float distanceFromPlayer;
	private float distanceFromParent;
	private float nodeWorth;// to determine if this Node is the closest to the player.

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform>();
		distanceFromPlayer = 0;
		distanceFromParent = 0;
		
		parentNode = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public NodeScript(List<NodeScript> newNodes){
		neighborNodes = newNodes;
	}
	
	public void CalculateNodeData(Transform goal){
		distanceFromPlayer = Vector3.Distance(transform.position, goal.position);//one of these variables here.///5. This is where the problem is supposed to originate from.
		if(parentNode != null){
			distanceFromParent = Vector3.Distance(transform.position, parentNode.ReturnPosition());
			distanceFromParent += parentNode.ReturnDistanceFromParent();//to calculate the total distance travelled if we took this path so far.
		}
		else{distanceFromParent = 0;}
		nodeWorth = distanceFromPlayer + distanceFromParent;//This is the total value of the node.
		
	}
	//This calculates how valuable this node is in checking for the player.
	public float ReturnNodeData(){
		return nodeWorth;
	}
	
	public void SetParent(NodeScript newParent){
		parentNode = newParent;
	}
	
	public NodeScript ReturnParent(){
		return parentNode;
	}
	
	public Vector3 ReturnPosition(){
		Debug.Log(transform.position);
		return transform.position;
	}
	
	public List<NodeScript> ReturnNeighbors(){
		return neighborNodes;
	}
	
	public void SetDistanceFromParent(float x){
		distanceFromParent = x;
	}
	public float ReturnDistanceFromParent(){
		return distanceFromParent;
	}
	
	/*public NodeScript ReturnNextNodeToCheck(Transform goal){
		NodeScript nodeToReturn;
		float nodeValue = 100000;
		//this goes through all of the nodes to check if they are the 
		foreach(NodeScript node in neighborNodes){//for every neighbor node
			if(node.ReturnNodeData(goal) < nodeValue //if it's the most valuable one we've checked,
			&& !checkedList.Contains(node)){//AND isn't a node that we've checked already.
				nodeToReturn = node;
				nodeValue = node.ReturnNodeData(goal);
			}
		}
		if(nodeToReturn == null){//if we didn't find any nodes that weren't already checked.
			return null;
		}
		else{//if we did find a node to check,
			checkedList.Add(nodeToReturn);//add this to the checked list,
			nodeToReturn.SetParentNode(GetComponent<NodeScript>());//set the parent of that node to this node
			return nodeToReturn;//return the next node to check.
		}
	}*/
}
