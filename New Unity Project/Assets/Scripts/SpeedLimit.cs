using UnityEngine;

public class SpeedLimit : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxVelocity = 5f;
    public Animator animator;

    private bool set = true;
  
    void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude >= 0)
        {
            Vector3 newVelocity = rb.velocity.normalized;
            newVelocity *= maxVelocity;
            rb.velocity = newVelocity;
            if (!set)
            {
                animator.SetBool("stopped", false);
                set = true;
            }
        }    

        if (rb.velocity.magnitude < 1 && set)
        {
            animator.SetBool("stopped", true);
            set = false;
        }
    }
}
