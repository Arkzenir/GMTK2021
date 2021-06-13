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
    public List<GameObject> attacks;
    //Public
    public GameObject attack;
    public float speed;
    public float attackDelay;
    public float attackRange;
    public float detectRange;
    public float attackFade;
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        attacks = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //If player not in range, return
        following = true;
        if (Vector2.Distance(target.transform.position, transform.position) > detectRange)
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
        
    }

    private void FixedUpdate()
    {
        if (!following) return;
        
        if (!attacking)
        {
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
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        attack.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        following = true;
    }
}
