using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour {

    public float hp;
    public bool dead;
    public Collider atkHitbox;

	// Use this for initialization
	void Start () {
        dead = false;
		hp = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        if(hp<=0f) dead = true;
        if(Input.GetKeyDown(KeyCode.Space)){ Attack(atkHitbox); }

	}

    void Hit(float dmg=1f)
    {
        hp-=dmg;
    }

    void Attack (Collider col)
    {
        Collider[] test = Physics.OverlapBox(col.bounds.center,col.bounds.extents,col.transform.rotation,LayerMask.GetMask("Ennemy","Neutral"));
        foreach(Collider c in test)
            {
                //Debug.Log(c.name);
                c.SendMessage("Hit",2f);
            }
    }
}
