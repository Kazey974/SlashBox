using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour {

    public Enemy enemy;
    public Transform player;

    float chaseRange = 5, attackRange = 2;
    string state = "idle";

    private void Awake()
    {
        enemy.setSPEED(2);
    }

    private void Update()
    {
        if(Vector3.Distance(player.position,transform.position) < attackRange && enemy.IsFacing())
        {
            if(state != "attack"){ Debug.Log(Time.time + "- attack"); state = "attack"; }
            setAttack();
        }
        else if (Vector3.Distance(player.position, transform.position) < chaseRange)
        {
            if(state != "chase"){ Debug.Log(Time.time + "- chase"); state = "chase"; }
            setChase();
        }
        else
        {
            if(state != "idle"){ Debug.Log(Time.time + "- idle"); state = "idle"; }
            setIdle();
        }
    }

    void setIdle()
    {
        if(Vector3.Distance(enemy.direction,transform.position)<=0.5) enemy.StopMove();
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
        enemy.pathfinder.setTarget(player.position);
        if(enemy.pathfinder.Pathfinding())
        {
            enemy.direction = enemy.pathfinder.getDestination();
        }
        enemy.StopMove();
        enemy.InputAttack();
    }
}