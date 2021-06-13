using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    //AI variables
    private GameObject target;
    private bool following = false;
    private bool attacking = false;
    private float attackTime = 0;
    private float fadeTime = 0;
    private bool setDead = false;
    private BoxCollider2D coll;
    public List<GameObject> attacks;
    //Public
    public GameObject attack;
    public Animator animator;
    public float speed;
    public float attackDelay;
    public float attackRange;
    public float detectRange;
    public float attackFade;
    public bool dead = false;
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        attacks = new List<GameObject>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.M))
        {
            Die();
        }
#endif
        //Remove previously creeated attack object
        if(fadeTime > 0f){
            fadeTime -= Time.deltaTime;
            if(fadeTime <= 0f){
                if (attacks.Any())
                {
                    Destroy(attacks[attacks.Count - 1]);
                    attacks.RemoveAt(attacks.Count - 1);
                    attacking = false;
                }
            }
        }

        //If player not in range, return
        following = true;
        if (Vector2.Distance(target.transform.position, transform.position) > detectRange || dead)
        {
            following = false;
            return;
        }
        
        //Reset Countdowns
        if(attackTime <= 0f) attackTime = attackDelay;
        if (fadeTime <= 0f) fadeTime = attackFade; 
        
        //Perform attack if still in range at that frame
        if(attackTime > 0f && Vector2.Distance(target.transform.position, transform.position) < attackRange){
            attackTime -= Time.deltaTime;
            if(attackTime <= 0f){
                attacking = true;
                Attack(target.transform);
            }
        }
        
        
    }

    private void FixedUpdate()
    {
        animator.SetBool("moving", false);
        if (!following || dead) return;
        
        Vector2 dir = target.transform.position - transform.position;
        animator.SetFloat("verticalDir", dir.y);
        
        if (!attacking)
        {
            animator.SetBool("moving", true);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (Vector2.Distance(target.transform.position, transform.position) < attackRange *  0.60f)
            {
                float dist = attackRange * 0.60f;
                transform.position = (transform.position - target.transform.position).normalized * dist + target.transform.position;
            }
        }
    }

    private void Attack(Transform targetTransform)
    {
        Vector2 attackPos = targetTransform.position - transform.position;
        attackPos.Normalize();
        attackPos *= Vector2.Distance(targetTransform.position, transform.position);
        attackPos += (Vector2)transform.position;
        
        GameObject attack = Instantiate(this.attack, transform);
        attacks.Add(attack);
        attack.transform.position = attackPos;
        
        Vector2 dir = targetTransform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        attack.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        following = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball") && !target.gameObject.GetComponent<BallControl>().spin)
        {
            Die();
        }
    }
    
    public void Die()
    {
        if (!setDead)
        {
            dead = true;
            animator.SetTrigger("isDead");
            setDead = true;
            coll.enabled = false;
        }
    }
}
