using UnityEngine;
using System.Collections;

public class ReuScript : MonoBehaviour {
	private Transform transform;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.Translate(0,Time.deltaTime,0);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			transform.Translate(0,-Time.deltaTime,0);
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.Translate(Time.deltaTime,0,0);
		}
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.Translate(-Time.deltaTime,0,0);
		}
	}
}
