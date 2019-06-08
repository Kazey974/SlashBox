using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_remplacement : MonoBehaviour {

    public GameObject from;
    public GameObject to;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Attack"))
        {
            Instantiate(to,from.transform.position,from.transform.rotation,transform).name = "Children";
            Destroy(from);
        }
        if(from==null) from = transform.Find("Children").gameObject;
	}
}
