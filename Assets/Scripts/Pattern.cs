using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour {

    public Enemy enemy;
    public Transform player;

    float chaseRange = 5, attackRange = 2, maxCD = 1.5f, curCD = 0f;
    string state = "idle";

    private void Awake()
    {
        enemy.setStats(15,15,5,1,2);
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position,transform.position);
        //AttackCooldown
        if(curCD > 0){ curCD -= Time.deltaTime; }else{ curCD = 0; }

        if(distance <= attackRange && enemy.IsFacing())
        {
            //if(state != "attack"){ Debug.Log(Time.time + "- attack"); state = "attack"; }
            if(curCD==0) setAttack();
        }
        if(distance <= chaseRange && distance > attackRange)
        {
            //if(state != "chase"){ Debug.Log(Time.time + "- chase"); state = "chase"; }
            setChase();
        }
        if(distance > chaseRange)
        {
            //if(state != "idle"){ Debug.Log(Time.time + "- idle"); state = "idle"; }
            setIdle();
        }
    }

    void setIdle()
    {
        float distance = Vector3.Distance(enemy.direction,transform.position);
        if(distance<=0.5) enemy.StopMove();
    }

    void setChase()
    {
        enemy.pathfinder.setTarget(player.position);
        if(enemy.pathfinder.Pathfinding())
        {
            enemy.direction = enemy.pathfinder.getDestination();
            enemy.InputMove();
        }
    }

    void setAttack()
    {
        curCD = maxCD;
        enemy.pathfinder.setTarget(player.position);
        if(enemy.pathfinder.Pathfinding())
        {
            enemy.direction = enemy.pathfinder.getDestination();
        }
        enemy.StopMove();
        enemy.InputAttack();
    }
}