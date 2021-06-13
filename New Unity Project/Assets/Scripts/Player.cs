using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    private float hp;
    private bool isDead;

    private SpriteRenderer _spriteRenderer;
    
    public float startHP;
    
    public Animator animator;

    private bool dieOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        hp = startHP;
        isDead = false;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

        _spriteRenderer.color = Color.Lerp(Color.red, Color.white, hp / startHP);
        
        //Can restart if dead
        if (Input.GetKeyDown(KeyCode.R) && isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
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
        animator.SetTrigger("isDead");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Attack>() || !other.gameObject.GetComponent<Collider2D>().isTrigger) return;
        other.gameObject.GetComponent<Attack>().TakeDamage(gameObject);
    }
    
}
