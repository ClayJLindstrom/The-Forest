using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class DogScript : MonoBehaviour {
	private Transform transform, player;
	public NodeScript startingNode;
	private PathFindingScript gps;
	private List<Transform> drawnPath;
	
	private float speed;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform>();
		player = GameObject.Find("Reu").GetComponent<Transform>();
		//Debug.Log(player.position.x);///The value gets returned here.
		//startingNode = GameObject.Find("Map").gameObject.transform.Find("Node Map").gameObject.transform.Find("Node(2)").GetComponent<NodeScript>();
		//Debug.Log(startingNode.ReturnNodeData(player));///Here, too. it can't be the Node, can it?
		gps = GetComponent<PathFindingScript>();
		drawnPath = gps.FindMap(startingNode, player);///Where the problem starts.
		
		speed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		//if we do have a map
		if(Vector3.Distance(drawnPath[drawnPath.Count -1].position, transform.position) < 0.3f){
			startingNode = drawnPath[drawnPath.Count - 1].gameObject.GetComponent<NodeScript>();
			if(drawnPath.Count == 1){
				drawnPath.Clear();
				drawnPath = gps.FindMap(startingNode, player);
				Debug.Log(drawnPath.Count);
			}
			else{
				drawnPath.Remove(drawnPath[drawnPath.Count -1]);
			}
		}
		else{
			if(drawnPath[drawnPath.Count -1] != null){
				//if the node is to the right
				if(drawnPath[drawnPath.Count -1].position.x > transform.position.x){
					transform.Translate(speed * Time.deltaTime, 0,0);
				}
				//if the node is to the left
				if(drawnPath[drawnPath.Count -1].position.x < transform.position.x){
					transform.Translate(-speed * Time.deltaTime, 0,0);
				}
				//if the node is up
				if(drawnPath[drawnPath.Count -1].position.y > transform.position.y){
					transform.Translate(0, speed * Time.deltaTime,0);
				}
				//if the node is down
				if(drawnPath[drawnPath.Count -1].position.y < transform.position.y){
					transform.Translate(0, -speed * Time.deltaTime,0);
				}
			}
		}
		
	}
	
	void OnCollisionEnter2D(Collision2D thing){
		if(thing.gameObject.tag == "Player"){SceneManager.LoadScene(SceneManager.GetActiveScene().name);}
	}
}
