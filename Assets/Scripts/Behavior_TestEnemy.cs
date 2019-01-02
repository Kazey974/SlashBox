using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Behavior_TestEnemy : MonoBehaviour {

    public Collider Los;
    public Collider Range;
    public Transform target;
    public Pathfinding path;
    public Animator animator;
    public Status s;
    public SpriteDisplay sd;
    
    public bool state_Idle, state_Combat, noLos, noRange;
    public float moveRange, moveTime, moveCd, nolosTime, nolosCd, atkCd, atkTime, idleTime, idleCd;
    int sec;
    Vector3 randMove;

    private void Start()
    {
        state_Idle  = true;
        state_Combat= false;
        target      = null;
        noLos       = true;
        noRange     = true;
        randMove    = transform.position;   moveRange = 12f;
          idleTime  = 0f; idleCd  = 3f;
          moveTime  = 0f; moveCd  = 5f;
          nolosTime = 0f; nolosCd = 2f;
          atkTime   = 0f; atkCd   = 3f;
    }

    private void Update()
    {
        //STATES
        Detect();

        if (state_Idle)
        {
            if(moveTime>0f) { Move(); }
            else { Idling(); }
        }
        if (state_Combat)
        {
            if(s.IsStat(new string[] { "idle" }))
            {
                if(noRange)
                {   //chase
                    Chase();
                } else {
                    //attack
                    if(atkTime<=0f) Attack();
                }
            }
            //leaveCombat
            if(noLos && nolosTime<=0f) toIdle();
        }
        
        
        if(atkTime>0f) { //AtkCooldown
            atkTime -= Time.deltaTime;
        } else if(s.IsStat(new string[] { "attacking" })) {
            s.ResetStatus();
        }
    }

    void toIdle()
    {
        state_Idle = true;
        state_Combat = false;
        moveTime = 0f;
        s.ResetStatus();
    }

    void toCombat()
    {
        state_Idle = false;
        state_Combat = true;
    }

    void Idling()
    {
        if(idleTime<=0f){
            if((int)Random.Range(1f, 2f) == 1){
                randMove = new Vector3(transform.position.x + Random.Range(-moveRange,moveRange),0f,transform.position.z + Random.Range(-moveRange,moveRange));
                moveTime = moveCd; }
            idleTime = idleCd;
        }else{ idleTime -= Time.deltaTime; }
    }

    void Move()
    {
        moveTime -= Time.deltaTime;
        transform.LookAt(randMove);
        path.SetDestination(randMove);
    }

    void Chase()
    {
        transform.LookAt(target);
        path.SetDestination(target.position);
    }

    void Attack()
    {
        sd.ClearTarget();
        sd.SetTarget(target);
        path.Stop();
        s.ChangeStatus("attacking");
        animator.Play("Attacking");
        atkTime = atkCd;
    }
    
    void Detect() //Search for targets in its range
    {
        if(nolosTime>0f) nolosTime -= Time.deltaTime;

        //Detect LoS range
        Collider[] inLos = Physics.OverlapSphere(Los.transform.position,Los.bounds.extents.magnitude,LayerMask.GetMask("Ally","Neutral"));
        foreach(Collider c in inLos)
        {   noLos = false;
            if(c.name=="Player") target = c.transform;
            toCombat();
        }   if(inLos.Length==0 && state_Combat && !noLos){ nolosTime = nolosCd; noLos = true; noRange = true; }

        //Detect Attack Range
        if(!noLos) 
        {   Collider[] inRange = Physics.OverlapBox(Range.bounds.center,Range.bounds.extents,Range.transform.rotation,LayerMask.GetMask("Ally","Neutral"));
            foreach(Collider c in inRange)
            {   if(c.name == target.name) { noRange = false; }
            }   if(inRange.Length==0) noRange = true;
        }
    }
}
