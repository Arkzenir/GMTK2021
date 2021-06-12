using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private bool set = false;
    private Transform target;

    void FixedUpdate()
    {
        if (target)
        {
            Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public bool SetTarget(Transform t)
    {
        if (t && !set)
        {
            target = t;
            set = true;
            return true;
        }
        return false;
    }
}
