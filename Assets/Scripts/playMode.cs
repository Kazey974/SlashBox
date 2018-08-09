using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMode : MonoBehaviour {

    public GameObject playzone;
    public float ratio;
    private Vector3 scale;

	// Use this for initialization
	void Start () {
		if(ratio==0f) ratio = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
	}

    // Widen the playzone
    public void widenZone()
    {
        scale = new Vector3(ratio,0f,ratio);
        ratio = ratio*2;
        playzone.transform.localScale += scale;
    }
}
