using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Behavior_TestEnemy : MonoBehaviour {

    public bool isIdle, isCombat, noLos;
    public Collider Los;
    public Collider Hitbox;
    public Transform target;
    public NavMeshAgent agent;
    public Animator animator;

    public float cd, time, randTime;
    int sec;
    Vector3 randMove;

    private void Start()
    {
        isIdle = true;
        isCombat = false;
        target = null;
        noLos = false;
        randMove = transform.position;

        cd = 0f; time = 0f; randTime = 0f;
    }

    private void Update()
    {
        Detect();

        if (isIdle)
        {
            //do nothing
            Idling();
            //move around
            if(Time.time<=time) Move();
        }
        if (isCombat)
        {
            //chase
            Chase();
            //attack
            
            //leaveCombat
            if(noLos && Time.time>=time) toIdle();
        }
    }

    void toIdle()
    {
        isIdle = true;
        isCombat = false;
    }

    void toCombat()
    {
        isIdle = false;
        isCombat = true;
    }

    void setCD(float t)
    {
        cd = t; time = Time.time + cd;
    }

    void Idling()
    {
        if(Time.time >= randTime)
        {
            if((int)Random.Range(1f, 2f) == 1)
            {
                randMove = new Vector3(transform.position.x + Random.Range(-6f,6f),0f,transform.position.z + Random.Range(-6f,6f));
                setCD(5f);
            }
            randTime = Time.time + 4f;
        }
    }

    void Move()
    {
        transform.LookAt(randMove);
        agent.SetDestination(randMove);
    }

    void Chase()
    {
        transform.LookAt(target);
        agent.SetDestination(target.position);
    }

    void Attack()
    {

    }

    void Detect()
    {
        Collider[] inLos = Physics.OverlapSphere(Los.transform.position,Los.bounds.extents.magnitude,LayerMask.GetMask("Ally","Neutral"));
        foreach(Collider c in inLos)
        {
            noLos = false;
            if(c.name=="Player") target = c.transform;
            toCombat();
        }
        if(inLos.Length==0 && isCombat && !noLos){ setCD(2f); noLos = true; }
    }
}
