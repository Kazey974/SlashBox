using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {

    public GameObject anchor;
    public float speed;
    private Vector3 targetPos;

	// Use this for initialization
	void Start () {
        speed = 0.05f;
	}
	
	// Update is called once per frame
	void Update () {
        targetPos = new Vector3(anchor.transform.position.x,5f,anchor.transform.position.z-10f);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed);
    }
}
