using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public CharacterController characterController;
    public Animator animator;
    public Vector3 direction;  //Direction entity must face

    float maxHP = 1000, currentHP = 1000,ATK = 1,SPEED = 10,DEF = 1,comboDelay = 0;
    bool isInvincible, isStunned, isAttacking, isMoving, isFacing;

    private void Awake()
    {
        direction = transform.forward + transform.position;
        isInvincible = false;
        isStunned = false;
        isAttacking = false;
        isMoving = false;
        isFacing = false;
    }

    void Update ()
    {
        //Entity is facing its direction ?
        //if((direction-transform.position).normalized != transform.forward.normalized) { setMoving(0); isFacing = false; } else { isFacing = true; }
        isFacing = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(direction - transform.position)) < 30;
		//Face towards facing direction (if not stunned or attacking)
        if(direction != transform.position && (direction-transform.position).normalized != transform.forward.normalized && !isStunned && !isAttacking)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.LookRotation(new Vector3(direction.x,0,direction.z)-new Vector3 (transform.position.x,0,transform.position.z)),600*Time.deltaTime);
        }

        //ResetAttack after delay (attack number)
        if (comboDelay != 0 && Time.time > comboDelay)
        {
            resetAttack();
        }
	}

    public void Move()  //Move the entity forward
    {
        if(!isStunned && !isAttacking && isFacing)
        {
            setMoving();
            characterController.Move((transform.forward + Vector3.down) * Time.deltaTime * SPEED);
        }
    }

    public void Attack()
    {
        if (!isStunned && !isAttacking)
        {
            animator.Play("Attack");
        }
    }

    public void TakeDamage(float inputDmg)
    {
        //Damage calculation
        float totalDmg = inputDmg - DEF;
        if(totalDmg>0) currentHP -= totalDmg;
        //Effect on entity
        animator.Play("TakeDamage");
    }

    public void Die()
    {
        animator.Play("Die");
    }

    //Animator Mapping
    public void setAttack(int b = 1)     {   animator.SetInteger("Attack",b); }
      public void setComboDelay(float t){   comboDelay = Time.time + t; }
      public void resetAttack()          {   setAttack(); comboDelay = 0; }
    public void setMoving(int b = 1)    {   bool boolean = b == 1 ? true:false; animator.SetBool("isMoving",boolean);     isMoving = boolean; }
    public void setInvincible(int b = 1){   bool boolean = b == 1 ? true:false; animator.SetBool("isInvincible",boolean); isInvincible = boolean; }
    public void setAttacking(int b = 1) {   bool boolean = b == 1 ? true:false; animator.SetBool("isAttacking",boolean);  isAttacking = boolean; }
    public void setStunned(int b = 1)   {   bool boolean = b == 1 ? true:false; animator.SetBool("isStunned",boolean);    isStunned = boolean; }
    public void resetStates(){ setMoving(0); setInvincible(0); setAttacking(0); setStunned(0); }

    //GetStats
    public float getATK()   { return ATK; }
    public float getSPEED() { return SPEED; } public void setSPEED(float s) { SPEED = s; }
    public bool IsFacing()  { return isFacing; }

    private void OnTriggerEnter(Collider col) //Contact with hitbox
    {
        Debug.Log("Object "+gameObject.name+"\n collided by "+col.name+" from "+col.transform.parent.name);
        if(col.gameObject.transform.parent != transform && col.name == "Hitbox")
        {
            if(!isInvincible) TakeDamage(col.GetComponent<Hitbox>().getDamage());
        }
    }

}