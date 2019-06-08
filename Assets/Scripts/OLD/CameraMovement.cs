using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public GameObject anchor;
    public float speed;
    private Vector3 targetPos;
    private float yOffset;
    private float zOffset;

	// Use this for initialization
	void Start () {
        speed = 1f;
        yOffset = transform.position.y;
        zOffset = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        if(anchor!=null){
        targetPos = new Vector3(anchor.transform.position.x,yOffset,anchor.transform.position.z+zOffset);
        transform.position = Vector3.Lerp(transform.position, targetPos, speed*Time.deltaTime);
        }
    }
}
