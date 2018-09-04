using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprite : MonoBehaviour {

    public Transform spriteOf;
    Quaternion orientation;
    Animator animator;
    Vector3 move;

	// Use this for initialization
	void Start () {
		orientation = Quaternion.Euler(45f,0f,0f);
        animator = GetComponent<Animator>();
        move = spriteOf.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Show sprite facing camera
		transform.rotation = orientation;
        transform.position = spriteOf.position;
        //Send position relative to transform
        isFacing(); isMoving();
	}

    void isFacing()
    {
        float r = spriteOf.eulerAngles.y;
        animator.SetFloat("rotation",r);
    }

    void isMoving()
    {
        if (move != spriteOf.position)
        {
            var pos = spriteOf.position;
            if(move.x == pos.x){ animator.SetFloat("x",0f); }else{ if(move.x<pos.x){ animator.SetFloat("x",1); } else { animator.SetFloat("x",-1); } }
            if(move.z == pos.z){ animator.SetFloat("z",0f); }else{ if(move.z<pos.z){ animator.SetFloat("z",1); } else { animator.SetFloat("z",-1); } }
            move = spriteOf.position;
        }
    }
}
