using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    //Contient toutes les touches/contrôles du joueur
    //et les conditions pour déclencher une action (bouger, attaquer...)

    public CharacterController c;
    public Status s;
    public Animator a;
    public Collider selfCollider;
    
	void Start () {
	}

    void Update () {
        if(C_Input()) {
            C_Attack();
        }
		if(M_Input()) {
            M_Walk();
            if(a.GetBool("walk")!=true){a.SetBool("walk",true);}
        } else {
            if(a.GetBool("walk")==true){a.SetBool("walk",false);}
            if(s.IsStat(new string[] {"idle","walk"})) s.ResetStatus();
        }
	}

#region Movement
    bool M_Input()
    {
        if ((Mathf.Abs(Input.GetAxis("Horizontal")) !=0 || Mathf.Abs(Input.GetAxis("Vertical")) != 0)) return s.IsStat(new string[] {"idle","walk"});
        return false;
    }
     void M_Walk()
    {
        s.ChangeStatus("walk");
        var direction = new Vector3(Input.GetAxis("Horizontal"),-s.fallSpeed,Input.GetAxis("Vertical"));
        transform.LookAt(new Vector3(Input.GetAxis("Horizontal")+transform.position.x,transform.position.y,Input.GetAxis("Vertical")+transform.position.z));
        c.Move(direction*Time.deltaTime*s.curSpeed);
    }
#endregion

#region Combat
    bool C_Input()
    {
        if (Input.GetButtonDown("Attack")) return s.IsStat(new string[]{"idle","walk"});
        return false;
    }

    public void C_Hit(float dmg=1f)
    {
        if(selfCollider.enabled)
        {
            a.SetTrigger("takeDamage");
        }
    }

    void C_Attack()
    {
        a.Play("Attack");
        s.ChangeStatus("attack");
    }
#endregion

}