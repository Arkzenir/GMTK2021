using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanger : MonoBehaviour
{
    private float FIRE_ANIM_LENGTH = 1.017f;
    
    //AI variables
    private GameObject target;
    private bool tracking = false;
    private bool attacking = false;
    private float attackTime = 0;
    //Reference
    public Transform bow;
    public Transform aim;
    public GameObject attack;
    private Animator animator;
    private Animator bowAnimator;
    //Public
    public float attackDelay;
    public float attackRange;
    public float detectRange;
    public float bowFloatDist;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        bowAnimator = bow.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //If player not in range, return
        tracking = true;
        if (Vector2.Distance(target.transform.position, transform.position) > detectRange)
        {
            tracking = false;
            bow.gameObject.SetActive(tracking);
            return;
        }

        bow.gameObject.SetActive(tracking);
        
        //Reset Countdowns
        if(attackTime <= 0f) attackTime = attackDelay;

        //Perform attack if still in range at that frame
        if (Vector2.Distance(target.transform.position, transform.position) < attackRange)
        {
            if (attackTime < FIRE_ANIM_LENGTH)
            {
                bowAnimator.SetBool("firing", true);
            }

            if (attackTime > 0f)
            {
                attackTime -= Time.deltaTime;
                if (attackTime <= 0f)
                {
                    attacking = true;
                    Attack(target.transform);
                }
            }
        }
    }
    
    private void FixedUpdate()
    {
        if (!tracking) return;
        
        Vector2 targetDir = target.transform.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 180;
        bow.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
        targetDir.Normalize();
        bow.localPosition = (targetDir * bowFloatDist);
    }
    
    private void Attack(Transform targetTransform)
    {
        GameObject attack = Instantiate(this.attack, transform);
        attack.transform.position = aim.position;
        
        //Orient object
        Vector2 dir = targetTransform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 180;
        attack.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        //Target the player
        attack.GetComponent<Projectile>().SetTarget(targetTransform.position);
        
        bowAnimator.SetBool("firing", false);
        tracking = true;
    }
}
