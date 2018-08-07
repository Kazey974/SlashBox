using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    public float mvSpeed;

	// Use this for initialization
	void Start () {
		mvSpeed = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Z)){ transform.position += Vector3.forward*mvSpeed*Time.deltaTime;}
		if(Input.GetKey(KeyCode.Q)){ transform.position += Vector3.left*mvSpeed*Time.deltaTime;   }
		if(Input.GetKey(KeyCode.S)){ transform.position += Vector3.back*mvSpeed*Time.deltaTime;   }
		if(Input.GetKey(KeyCode.D)){ transform.position += Vector3.right*mvSpeed*Time.deltaTime;  }
	}
}
