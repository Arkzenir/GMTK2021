using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement = Vector2.zero;
    
    //Animation control parameters 
    public Animator animator;
    private bool isWalking;
    private bool lookingLeft = false;
    private SpriteRenderer _spriteRenderer;

    private Player playerScript;
    private void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerScript = GetComponent<Player>();
    }

    void Update()
    {
        //Deny input if dead
        if (playerScript != null && playerScript.IsDead()) return;
        //Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (movement.x != 0
            && movement.y != 0)
        {
            rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime / Mathf.Sqrt(2));
        }
        else
        {
            rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
        }

        if (lookingLeft && movement.x > 0 )
        {
            _spriteRenderer.flipX = !lookingLeft;
            lookingLeft = false;
        }
        else if (!lookingLeft && movement.x < 0)
        {
            _spriteRenderer.flipX = !lookingLeft;
            lookingLeft = true;
        }

        
        animator.SetBool("isWalking",!movement.Equals(Vector2.zero));
    }
    
    
}
