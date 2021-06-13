using System;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public bool spin = false;
    public bool release = false;
    public Animator animator;
    


    // Update is called once per frame
    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButton("Fire1"))
        {
            if (collision.gameObject.tag == "Ball"
                && spin == false)
            {
                spin = true;
            }
        
        }
        
    }
    
    

    void FixedUpdate()
    {
        if (!Input.GetButton("Fire1"))
        {
            animator.SetBool("powerUsed", false);
        }
        else
        {
            animator.SetBool("powerUsed", true);
        }
        
        if (Input.GetMouseButton(1)
        && spin)
        {
            spin = false;
            release = true;

        }
    }
}   
