using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    public Pathfinder pathfinder;

    float maxStamina = 500, curStamina = 500;
    bool setMove;

    private void LateUpdate()
    {
        if(curStamina<maxStamina) curStamina++;
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
        if (curStamina > 0)
        {
            Attack();
        }
    }

    public void DrainStamina(float x = 100)
    {
        curStamina -= x;
    }
}
