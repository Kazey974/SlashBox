using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteDisplay : MonoBehaviour {
    //Gère l'affichage des personnages
    
    Transform reference;
    Quaternion orientation;
    public Animator animator;
    public Collider col;
    public Status status;
    public AudioSource audio;
    public AudioClip[] sounds;
    Vector3 move;

    public GameObject[] spawning;
    public Stack<Transform> target;

	void Start () {
        if(spawning==null) spawning = new GameObject[0];
        if(sounds==null) sounds = new AudioClip[0];
        target = new Stack<Transform>();
        reference = transform.parent;
        move = reference.position;
		orientation = Quaternion.Euler(45f,0f,0f);
	}

#region AFFICHAGE
	void LateUpdate () {
        //Show sprite facing camera
		transform.rotation = orientation;
        //transform.position = reference.position;
        //Send position relative to transform
        isFacing(); isMoving();
	}

    void isFacing()
    {
        float r = reference.eulerAngles.y;
        animator.SetFloat("rotation",r);
    }

    void isMoving()
    {
        if (move != reference.position)
        {
            var pos = reference.position;
            if(move.x == pos.x){ animator.SetFloat("x",0f); }else{ if(move.x<pos.x){ animator.SetFloat("x",1); } else { animator.SetFloat("x",-1); } }
            if(move.z == pos.z){ animator.SetFloat("z",0f); }else{ if(move.z<pos.z){ animator.SetFloat("z",1); } else { animator.SetFloat("z",-1); } }
            move = reference.position;
        }
    }
#endregion

    //SPAWN -   Liste des sous-éléments du personnage
    //          Contient les attaques, invocations et autres à déclencher lors des animations
    void Spawn(int x)
    {
        foreach(Transform t in target)
        { Instantiate<GameObject>(spawning[x],t.position,Quaternion.identity); }
    }
    
    //TARGET -  Ajoute une cible à la liste, permet d'avoir une référence pour les attaques
    public void SetTarget(Transform t)
    {
        target.Push(t);
    }

    //TARGET -  Vide la liste de cibles, à appeler pendant tout nouveau ciblage
    public void ClearTarget()
    {
        target.Clear();
    }
    
    //COLLIDER - du personnage (référence)
    //          Permet d'activer ou désactiver le collider OU de le toggle (pourquoi ?)
    void ToggleCollider(int x = -1)
    {
        if (col != null)
        {
            if(x==0) col.enabled = false;
            if(x==1) col.enabled = true;
            if(x==-1)col.enabled = !col.enabled;
        }
    }

    //STATUS
    void SetIdle()
    {
        if(status!=null) status.ResetStatus();
    }

    void Dead()
    {
        Destroy(transform.parent.gameObject);
    }

    //SOUNDS -  Permet de jouer un son lors d'une animation
    void PlaySound(int x)
    {
        audio.clip = sounds[x];
        audio.Play();
    }

    //SOUNDS -  Ou un son aléatoire dans une fourchette (exemple : 1 à 3)
    void PlayRandomSound(string s)
    {
        int x = int.Parse(s.Split(',')[0]);
        int y = int.Parse(s.Split(',')[1]);
        audio.clip = sounds[(int)Random.Range(x,y)];
        audio.Play();
    }
}
