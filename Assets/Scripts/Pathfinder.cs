using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    public Vector3 target;
    
    bool canFind;
    Vector3 destination;

    public void setTarget(Vector3 t)
    {
        target = t;
    }

    public Vector3 getDestination()
    {
        return destination;
    }

    public bool Pathfinding()
    {
        if(!canSeeTarget(transform.position)){
            List<Vector3> origin = getFarPoints(transform.position);
            List<Vector3> close = new List<Vector3>();
            List<Vector3> last = new List<Vector3>();
            foreach(Vector3 p in origin)
            {
                //Debug.DrawRay(p,Vector3.up,Color.red);
                close.Add(closestPoint(transform.position,p,target));
            }
            foreach(Vector3 p in close)
            {
                if (canSeeTarget(p))
                {
                    last.Add(p);
                }
            }
            destination = pathLength(last);
            return canFind;
        }
        else
        {
            destination = target;
            return true;
        }
    }

    List<Vector3> getFarPoints(Vector3 from)
    {
        RaycastHit hit;
        Vector3 raydir = Vector3.forward;
        List<Vector3> results = new List<Vector3>();
        
        for(int i = 0; i < 360; i++){
            Vector3 p1 = Vector3.zero, p2 = p1, p3 = p1;
            float d1 = 0, d2 = 0, d3 = 0;
            
            //Get 3 points, take furthest
            if(Physics.Raycast(transform.position,raydir,out hit, Mathf.Infinity)){ p2 = hit.point; d2 = Vector3.Distance(transform.position,hit.point); }
            if(Physics.Raycast(transform.position,Quaternion.Euler(Vector3.down) * raydir,out hit, Mathf.Infinity)){ p1 = hit.point; d1 = Vector3.Distance(transform.position,hit.point); }
            if(Physics.Raycast(transform.position,Quaternion.Euler(Vector3.up) * raydir,out hit, Mathf.Infinity)){ p3 = hit.point; d3 = Vector3.Distance(transform.position,hit.point); }

            if(d1!=0 && d2!=0 && d3!=0){
                if(d1<d2&&d2>d3)
                {
                    results.Add(p2-raydir);
                    //Debug.DrawRay(p2,Vector3.up,Color.yellow);
                }
            }
            raydir = Quaternion.Euler(Vector3.up) * raydir;
        }

        return results;
    }

    bool canSeeTarget(Vector3 from)
    {
        RaycastHit hit;
        Vector3 raydir = (target-from).normalized;
        float dist = Vector3.Distance(from, target)-1;
        return (!Physics.Raycast(from,raydir,out hit,dist));
    }

    /*bool canSeeOrigin(Vector3 from)
    {
        RaycastHit hit;
        Vector3 raydir = (from - transform.position).normalized;
        float dist = Vector3.Distance(from, transform.position)-1;
        return (!Physics.Raycast(from,raydir,out hit,dist));
    }*/

    /*bool isValid(Vector3 from)
    {
        bool valid = false;
        if(can)
        if(canSeeTarget(from)) valid = true;
        return valid;
    }*/

    Vector3 pathLength(List<Vector3> path)
    {
        float r = 0; int i = 0;
        Vector3 dest = Vector3.positiveInfinity;
        foreach(Vector3 p in path)
        {
            //Debug.DrawRay(p,Vector3.up,Color.gray);
            float d = 0;
            d += Vector3.Distance(transform.position,p);
            d += Vector3.Distance(p,target);
            if(r==0){ r = d; if(canSeeTarget(p)) dest = path[i]; }
            if(d<r){ r = d; if(canSeeTarget(p)) dest = path[i]; }
            i++;
        }
        //Debug.DrawRay(dest,Vector3.up,Color.white);
        if(dest==Vector3.positiveInfinity){canFind = false; } else { canFind = true; }
        return dest;
    }

    /*List<Vector3> findPath(Vector3 from)
    {

    }*/

    Vector3 closestPoint(Vector3 from, Vector3 to, Vector3 t) //Point le plus proche ayant visibilité sur le suivant
    {
        float dist = Vector3.Distance(from,to);
        Vector3 dir = (from - to).normalized;
        Vector3 p1 = to, p2 = to + dir;
        for(float i = 0;i < dist; i++){
            Vector3 raydir1 = (t - p1).normalized;
            Vector3 raydir2 = (t - p2).normalized;
            if(!Physics.Raycast(p1, raydir1, Vector3.Distance(p1, t)-1)
            &&Physics.Raycast(p2, raydir2, Vector3.Distance(p2, t)-1))
            {
                break;
            }
                //Debug.DrawRay(p2,Vector3.up,Color.red);
            p1 = p2; p2 += dir;
        }
        //Debug.DrawRay(p1-dir,Vector3.up,Color.blue);
        return p1-dir;
    }
}
