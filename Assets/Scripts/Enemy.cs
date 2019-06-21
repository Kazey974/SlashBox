using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    public Pathfinder pathfinder;

    bool setMove;

    private void LateUpdate()
    {
        if(setMove){ Move(); }else{ setMoving(0); }
    }

    public void InputMove()
    {
        setMove = true;
    }

    public void StopMove()
    {
        setMove = false;
    }

    public void InputAttack()
    {
        Attack();
    }

    public void DeleteEnemy()
    {
        gameObject.SetActive(false);
    }
}
