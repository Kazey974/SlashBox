using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDisplay : MonoBehaviour {
    
    Transform reference;
    Quaternion orientation;
    public Animator animator;
    public Collider collider;
    public Status status;
    Vector3 move;

    public GameObject[] Spawning;
    public Stack<Transform> Target;

	void Start () {
        if(Spawning==null) Spawning = new GameObject[1];
        Target = new Stack<Transform>();
        reference = transform.parent;
        move = reference.position;
		orientation = Quaternion.Euler(45f,0f,0f);
	}
	
	void LateUpdate () {
        //Show sprite facing camera
		transform.rotation = orientation;
        //transform.position = reference.position;
        //Send position relative to transform
        isFacing(); isMoving();
	}

    void isFacing()
    {
        float r = reference.eulerAngles.y;
        animator.SetFloat("rotation",r);
    }

    void isMoving()
    {
        if (move != reference.position)
        {
            var pos = reference.position;
            if(move.x == pos.x){ animator.SetFloat("x",0f); }else{ if(move.x<pos.x){ animator.SetFloat("x",1); } else { animator.SetFloat("x",-1); } }
            if(move.z == pos.z){ animator.SetFloat("z",0f); }else{ if(move.z<pos.z){ animator.SetFloat("z",1); } else { animator.SetFloat("z",-1); } }
            move = reference.position;
        }
    }

    void Spawn(int x)
    {
        foreach(Transform t in Target)
        { Instantiate<GameObject>(Spawning[x],t.position,Quaternion.identity); }
    }

    public void ClearTarget()
    {
        Target.Clear();
    }

    public void SetTarget(Transform t)
    {
        Target.Push(t);
    }

    void ToggleCollider()
    {
        if (collider != null)
        {
            collider.enabled = !collider.enabled;
        }
    }

    void SetIdle()
    {
        if(status!=null) status.ResetStatus();
    }

    void Dead()
    {
        Destroy(transform.parent.gameObject);
    }
}
