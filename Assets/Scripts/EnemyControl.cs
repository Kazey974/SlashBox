using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
 
    private float atkCd, atkTimer, losCd, losTimer;
    private bool dead, onrange;
    public Collider atkHitbox;
    public Collider lineOfSight;
    private GameObject target;
    public CharacterController controller;
    public MonoBehaviour behavior;
    public Status s;
    
	void Start () {
        dead = false; onrange = false;
        target = null;
        atkCd = 4f; atkTimer = 0f;
        losCd = 5f; losTimer = 0f;
	}
	
	void Update () {
    //Object is dead (hp<=0)
        if(s.hp<=0f) { dead = true; } else {
       /* //Checks if target is on range
            onrange = IsOnRange(atkHitbox);
        //Target not acquired
            if(target==null || Time.time > losTimer) { target = IsOnSight(lineOfSight); }
        //Has a target
            else 
            {   
                Combat(target);
            }*/
        }
	}

    void Hit(float dmg=1f)
    {
        s.hp-=dmg;
    }

    bool IsOnRange(Collider hit)
    {
        Collider[] test = Physics.OverlapBox(hit.bounds.center,hit.bounds.extents,hit.transform.rotation,LayerMask.GetMask("Ally","Neutral"));
        foreach(Collider c in test)
            {
                if(c.name == target.name) { return true; }
            }
        return false;
    }

    GameObject IsOnSight (Collider los)
    {
        Collider[] test = Physics.OverlapSphere(los.transform.position,los.bounds.extents.magnitude,LayerMask.GetMask("Ally","Neutral"));
        foreach(Collider c in test)
            {
                losTimer = Time.time + losCd;
                return c.gameObject;
            }
        return null;
    }

    void Combat(GameObject t)
    {
        if(onrange && Time.time > atkTimer) { Attack(t); atkTimer = Time.time + atkCd; }
        if(Vector3.Distance(t.transform.position,transform.position)>1.5f) { Walk(t.transform); }
    }

    void Walk(Transform towards)
    {
        transform.LookAt(towards);
        controller.Move(transform.forward*Time.deltaTime*3f);
    }

    void Attack (GameObject t)
    {
        t.SendMessage("C_Hit",s.atk);
        onrange = false;
    }
}
