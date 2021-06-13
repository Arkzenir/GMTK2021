using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour,IProjectile
{
    // Start is called before the first frame update
    public float speed;
    private bool set = false;
    private Vector2 target;
    private Vector2 dir;

    void FixedUpdate()
    {
        if (target != Vector2.zero)
        {
            transform.position = (Vector2)transform.position + dir * speed * Time.deltaTime;
        }
    }

    public bool SetTarget(Vector2 pos)
    {
        if (pos != Vector2.zero && !set)
        {
            target = pos;
            dir = target - (Vector2)transform.position;
            dir.Normalize();
            set = true;
            return true;
        }
        return false;
    }
    
    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
