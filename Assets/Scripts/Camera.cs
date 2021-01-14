using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	private Transform transform, player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform>();
		player = GameObject.Find("Reu").GetComponent<Transform>();
		offset = new Vector3(0,0,-10);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.position + offset;
	}
}
