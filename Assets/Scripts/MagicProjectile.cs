using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour {
    
    public float lifetime;
    public Animator animator;

	void Start () {
		lifetime = 10f;
	}
	
	void Update () {
		if(lifetime<=0f) { animator.SetBool("destroy",true);
        } else { lifetime -= Time.deltaTime; }
	}

    private void OnTriggerStay(Collider col)
    {
        if(col.transform.name == "Player")
        {
            PlayerControl PC = col.gameObject.GetComponent<PlayerControl>();
            PC.C_Hit();
        }
    }
}
