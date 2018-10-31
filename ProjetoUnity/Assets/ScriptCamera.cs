using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour {
	Vector3 velocity     = new Vector3(15.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += velocity * Time.fixedDeltaTime;
	}
}
