using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

    public Entity parent;
    public float multiplier;

    public float getDamage()
    {
        if(multiplier==0) multiplier = 1;
        return parent.getATK() * multiplier;
    }
}
