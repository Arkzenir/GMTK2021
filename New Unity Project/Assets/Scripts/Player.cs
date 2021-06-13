using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private float hp;
    private bool isDead;
    public float startHP;
    
    
    public Animator animator;

    private bool dieOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        hp = startHP;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.K))
        {
            SetIsDead(!isDead);
            Debug.Log("Player is dead: " + IsDead());
        }
#endif
        if (hp <= 0 || isDead)
        {
            if (dieOnce)
            {
                SetIsDead(true);
                dieOnce = false;
            }
        }
    }
    
    

    public float GetHP()
    {
        return hp;
    }

    public void SetHP(float value)
    {
        hp = value;
        Debug.Log("HP is: " + hp);
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void SetIsDead(bool val)
    {
        isDead = val;
        animator.SetBool("isDead", val);
        animator.SetBool("ghost", val);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.GetComponent<Attack>()) return;
        other.gameObject.GetComponent<Attack>().TakeDamage(gameObject);
    }
}
