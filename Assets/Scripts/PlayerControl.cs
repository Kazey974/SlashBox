﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    
    public CharacterController c;
    public Status s;
    public Animator a;
    public Collider selfCollider;
    
	void Start () {
	}

	void Update () {
        if(C_Input()) {
            //C_Attack();
        }
		else if(M_Input()) {
            M_Walk();
            if(a.GetBool("walking")!=true){a.SetBool("walking",true); Debug.Log("A");}
        }
        else {
            if(a.GetBool("walking")==true){a.SetBool("walking",false);}
            s.ResetStatus();
        }
	}

#region Movement
    bool M_Input()
    {
        if ((Mathf.Abs(Input.GetAxis("Horizontal")) !=0 || Mathf.Abs(Input.GetAxis("Vertical")) != 0)) return s.IsStat(new string[] {"idle","move"});
        return false;
    }
     void M_Walk()
    {
        s.ChangeStatus("move");
        var direction = new Vector3(Input.GetAxis("Horizontal"),-s.fallSpeed,Input.GetAxis("Vertical"));
        transform.LookAt(new Vector3(Input.GetAxis("Horizontal")+transform.position.x,transform.position.y,Input.GetAxis("Vertical")+transform.position.z));
        c.Move(direction*Time.deltaTime*s.curSpeed);
    }
#endregion

#region Combat
    bool C_Input()
    {
        if (Input.GetButtonDown("Attack")) return s.IsStat(new string[]{"idle","move"});
        return false;
    }

    public void C_Hit(float dmg=1f)
    {
        if(selfCollider.enabled)
        {
            a.SetTrigger("anim_dmg");
            Debug.Log("Hit");
        }
    }

    void C_Attack (Collider col)
    {
        s.ChangeStatus("atk");
        s.atk_time = Time.time;
        Collider[] test = Physics.OverlapBox(col.bounds.center,col.bounds.extents,col.transform.rotation,LayerMask.GetMask("Ennemy","Neutral"));
        foreach(Collider c in test)
            {
                //Debug.Log(c.name);
                c.SendMessage("C_Hit",2f);
            }
    }
#endregion

}