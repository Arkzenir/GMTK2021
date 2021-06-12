using UnityEngine;

public class SpeedLimit : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxVelocity = 5f;

  
    void FixedUpdate()
    {
        if (rb.velocity.sqrMagnitude >= 0)
        {
            Vector3 newVelocity = rb.velocity.normalized;
            newVelocity *= maxVelocity;
            rb.velocity = newVelocity;
        }
    }
}
