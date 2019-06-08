using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	void LateUpdate () {
        
        if(Mathf.Abs(Input.GetAxis("Horizontal"))==1 || Mathf.Abs(Input.GetAxis("Vertical"))==1)
        {
            setDirection();
        }

        if(Mathf.Abs(Input.GetAxis("Horizontal"))!=0 || Mathf.Abs(Input.GetAxis("Vertical"))!=0)
        {
            Move();
        }
        else
        {
            setMoving(0);
        }
        //Attack player input
        if(Input.GetButtonUp("Attack")) Attack();
	}

    void setDirection() //Set Direction according to player inputs
    {
        direction = transform.position + new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical"));
    }
}
