using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour {
    
    public float lifetime;
    public Collider mpCollider;

	void Start () {
		lifetime = 10f;
	}
	
	void Update () {
        lifetime -= Time.deltaTime;
		if(lifetime<=0f) Destroy(gameObject);
	}

    private void OnTriggerEnter(Collider col)
    {
        if(col.transform.name == "Player")
        {
            Debug.Log("Hit !");
            PlayerControl PC = col.gameObject.GetComponent<PlayerControl>();
            PC.C_Hit();
        }
    }
}
