using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
    
    Vector3 destination, move;
    public CharacterController c;

	void Start () {
        destination = transform.position;
	}
	
	void Update () {
        Debug.DrawRay(transform.position,move,Color.magenta);
        if(isObstacle()) findOtherWay();
        if (Vector3.Distance(destination,transform.position)>1.5f)
        {
            c.Move(move*Time.deltaTime*5f);
        }
	}

    public void SetDestination(Vector3 d)
    {
        destination = d;
    }
    
    public void Stop()
    {
        move = Vector3.zero;
    }

    bool isObstacle()
    {
        Vector3 from = transform.position;
        RaycastHit hit;
        Vector3 rayDir = (destination - from).normalized;
        float range = Vector3.Distance(destination,from) -1f;
        if(Physics.Raycast(from,rayDir,out hit, range))
        {
            Debug.DrawRay(from,rayDir*hit.distance,Color.red);
            return true;
        }
        else
        {
            move = new Vector3(destination.x - transform.position.x, 0f, destination.z - transform.position.z).normalized;
            return false;
        }
    }

    void findOtherWay()
    {
        Vector3 from = transform.position;
        List<Vector3> results = new List<Vector3>();
        List<Vector3> except = new List<Vector3>();
        List<Vector3> removeList = new List<Vector3>();
        except.Add(from);
        List<Vector3> farPoints = selectFarpoints(from);
        foreach(Vector3 fp in farPoints) if(!validPoint(fp,except)) removeList.Add(fp);
        foreach(Vector3 rm in removeList) farPoints.Remove(rm);
        removeList.Clear();
        if(farPoints.Count>1)
        {   float d = Vector3.Distance(farPoints[0],destination);
            for(int i=0;i<farPoints.Count;i++)
            {
                if (Vector3.Distance(destination, farPoints[i]) < d)
                {
                    d = Vector3.Distance(farPoints[i],destination);
                    removeList.Add(farPoints[i-1]);
                }
                if(Vector3.Distance(destination, farPoints[i]) > d)
                {
                    removeList.Add(farPoints[i]);
                }
            }
            foreach(Vector3 rm in removeList) farPoints.Remove(rm);
        }
        if(farPoints.Count==1){ move = new Vector3(farPoints[0].x-from.x,0f,farPoints[0].z-from.z).normalized; }
    }

    List<Vector3> selectFarpoints(Vector3 from) //prend tous les "angles" autour de l'origine et retourne ceux qui sont dans une autre pièce ou qui ont vue sur la cible
    {
        RaycastHit hit;
        List<Vector3> results = new List<Vector3>();
        Vector3 rayDir = Vector3.forward;

        List<Vector3> collisions = new List<Vector3>();
        for(int r = 0; r < 360; r++) //Raycast sur 360°
        {
            rayDir = Quaternion.Euler(Vector3.up) * rayDir;
            if(Physics.Raycast(from,rayDir,out hit, Mathf.Infinity))
            {
                //Debug.DrawLine(from,hit.point,Color.white);
                collisions.Add(hit.point-rayDir);
            }
        }
        
        int prev, next;
        for(int cur = 0; cur < collisions.Count; cur++) //Prendre tous les points les plus éloignés
        {
            if(cur==collisions.Count-1){ next = 0; }else{ next = cur +1; }
            if(cur==0){ prev = collisions.Count-1; }else{ prev = cur -1; }
            if (Vector3.Distance(from, collisions[prev]) <= Vector3.Distance(from, collisions[cur]) && Vector3.Distance(from, collisions[cur]) >= Vector3.Distance(from, collisions[next]))
            {
                //Debug.DrawRay(from,(collisions[cur]-from).normalized*Vector3.Distance(from,collisions[cur]),Color.yellow);
                results.Add(collisions[cur]);
            }
        }

        return results;
    }

    bool hasSight(Vector3 x)
    {
        if (Physics.Raycast(x, (destination - x).normalized, Vector3.Distance(destination, x)-1f)){ return false; } else { Debug.DrawRay(x,Vector3.up*4,Color.green); return true; }
    }

    bool isValid(Vector3 x, List<Vector3> except)
    {
        RaycastHit hit;
        bool validExcept = true;
        if(Physics.Raycast(x, (destination-x).normalized, out hit, Vector3.Distance(destination, x)-1f))
        {
            foreach(Vector3 e in except){
                if(!Physics.Raycast(hit.point, (e-hit.point).normalized, Vector3.Distance(e,hit.point)-1f))
                {
                    validExcept = false;
                }
            }
            if(validExcept) Debug.DrawRay(x,Vector3.up*4,Color.cyan);
            return validExcept;
        }else{
            return true;
        }
    }

    bool validPoint(Vector3 point, List<Vector3> except)
    {
        List<Vector3> removeList = new List<Vector3>();
        if(hasSight(point)) {
            Debug.DrawLine(point,destination,Color.green);
            return true;
        } else {
            if(!isValid(point,except)) {
                return false;
            } else {
                except.Add(point);
                List<Vector3> list2 = selectFarpoints(point);
                foreach(Vector3 point2 in list2)
                {
                    if(!validPoint(point2, except)) removeList.Add(point2);
                } foreach(Vector3 rm in removeList) list2.Remove(rm);
                if(list2.Count==0){ return false; } else { return true; }
            }
        }
    }
}