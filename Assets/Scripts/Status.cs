using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

    //Status Variables
    string stat;
    //lvl-related stats
    public float hp, atk;
    //intern stats
    public float mvSpeed, fallSpeed, fallRatio, curFall, curSpeed, st_time, st_cd;
    
    //test temp stats
    public float atk_time, atk_cd;

	void Awake () {
		stat = "idle";
	}

    void Start()
    {
		mvSpeed = 10f;
        curSpeed = mvSpeed;
        fallSpeed = 4f;
        fallRatio = 0.3f;
        atk_time = 0f;
        atk_cd = 1f;
        st_time = 0f;
        st_cd = 0f;
    }

//	void Update () {
//      ResetStatus();
//	}

//  public void ChangeStatus(string to,float cd = 0f)
    public void ChangeStatus(string to)
    {
//      Debug.Log(stat + "<-" + to);
//      st_time = Time.time; st_cd = cd;
        stat = to;
    }
    
    public void ResetStatus(string param = null)
    {
//      if((Time.time>=st_time+st_cd)&&(param==null||stat==param))
//      {
            stat = "idle";
//          st_cd = 0f; 
//      }
    }

    public bool IsStat(string []check)
    {
        //if(stat!="idle") Debug.Log(stat + " cd = " + st_cd);
        return (System.Array.IndexOf(check,stat)!= -1);
    }

//  void M_Fall()
//  {
//      s.ChangeStatus("falling");
//      c.Move(new Vector3(0,-s.curFall,0)*Time.deltaTime);
//      s.curFall += s.fallRatio;
//  }
}
