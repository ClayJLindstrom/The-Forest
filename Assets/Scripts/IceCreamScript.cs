using UnityEngine;
using System.Collections;

public class IceCreamScript : MonoBehaviour {
	private Transform transform;
	public Vector3 caughtPosition;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D finder){
		if(finder.gameObject.tag == "Player"){
			transform.position = caughtPosition;
			GetComponent<Collider2D>().enabled = false;
		}
	}
}
