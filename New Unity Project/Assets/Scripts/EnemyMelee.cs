using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    //AI variables
    private GameObject target;
    private bool attacking = false;
    private float attackTime = 0;
    private float fadeTime = 0; 
    private List<GameObject> attacks;
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
    }

    // Update is called once per frame
    void Update()
    {
        //If player not in range, return
        if (Vector2.Distance(target.transform.position, transform.position) > detectRange)
        {
            attacking = false;
            return;
        }
        //Reset Countdowns
        if(attackTime <= 0f) attackTime = attackDelay;
        if (fadeTime <= 0f) fadeTime = attackFade; 
        
        //Perform attack if still in range at that frame
        if(attackTime > 0f){
            attackTime -= Time.deltaTime;
            if(attackTime <= 0f){
                Attack(target.transform);
            }
        }
        
        //Remove previously creeated attack object
        if(fadeTime > 0f){
            fadeTime -= Time.deltaTime;
            if(fadeTime <= 0f){
                if(attacks.Count > 0)
                    attacks.RemoveAt(attacks.Count - 1);
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (!attacking) return;
    }

    private void Attack(Transform targetTransform)
    {
        Vector2 attackPos = target.transform.position - transform.position;
        attackPos.Normalize();
        attackPos *= attackRange;
        
        GameObject attack = Instantiate(this.attack, gameObject.transform);
        attacks.Add(attack);
        attack.transform.position = attackPos;
        attack.transform.LookAt(target.transform, Vector3.up);
        attacking = true;
    }
}
