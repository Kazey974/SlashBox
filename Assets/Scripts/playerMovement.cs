using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    public float mvSpeed, fallSpeed, fallRatio;
    private float curFall, curSpeed;
    private CharacterController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
		mvSpeed = 10f;
        curSpeed = mvSpeed;
        fallSpeed = 4f;
        fallRatio = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
        Walk();
	}

     void Walk()
    {
        if (controller.isGrounded)
        {
            //NotFalling
            curFall = fallSpeed;
            //Walking
            var direction = new Vector3(Input.GetAxis("Horizontal"),-fallSpeed,Input.GetAxis("Vertical"));
            if (Mathf.Abs(Input.GetAxis("Horizontal")) !=0 || Mathf.Abs(Input.GetAxis("Vertical")) !=0)
                { transform.LookAt(new Vector3(Input.GetAxis("Horizontal")+transform.position.x,transform.position.y,Input.GetAxis("Vertical")+transform.position.z)); }
            controller.Move(direction*Time.deltaTime*curSpeed);
        }
        else
        {
            Falling();
        }
    }
    
    void Falling()
    {
        controller.Move(new Vector3(0,-curFall,0)*Time.deltaTime);
        curFall += fallRatio;
    }
}
