using System;
using UnityEngine;

public class Attack : MonoBehaviour, IHurt
{
    public float damage;
    public float speed;
    public bool projectile = false;
    public bool aoe;
    private bool once;

    private void Start()
    {
        once = !aoe;
    }
    
    public bool TakeDamage(GameObject target)
    {
        if (!target)
        {
            return false;
        }
        else
        {
            if (!once)
            {
                target.GetComponent<IDamageable>().SetHP(target.GetComponent<IDamageable>().GetHP() - damage / Time.deltaTime);
            }
            else
            {
                target.GetComponent<IDamageable>().SetHP(target.GetComponent<IDamageable>().GetHP() - damage);
                if (projectile)
                {
                    Destroy(gameObject);
                }
                once = false;
            }
            
            return true; //Attack has connected
        }
    }
    
}