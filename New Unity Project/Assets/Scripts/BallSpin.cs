
using UnityEditor;
using UnityEngine;

public class BallSpin : MonoBehaviour
{
    public Transform target;
    public float orbitDistance = 10.0f;
    public float orbitDegreesPerSec = 180.0f;
    public Vector3 axis ;
    public Vector3 relativeDistance = Vector3.zero;
    public bool once = true;
    public Rigidbody2D rb;
    void Start()
    {
        if(target != null) 
        {
            relativeDistance = transform.position - target.position;
        }


    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameObject.FindWithTag("Player").GetComponent<BallControl>().spin)
        {
            axis = new Vector3(0, 0, 1);
            rb.velocity = Vector2.zero;
            if (target != null)
            {
                transform.position = (target.position + relativeDistance);
                transform.RotateAround(target.position, axis, orbitDegreesPerSec);
            }
            if (once)
            {
                var newPos = (transform.position - target.position).normalized * orbitDistance;
                newPos += target.position;
                transform.position = newPos;
                once = false;
            }
            relativeDistance = transform.position - target.position;
        }

        if (GameObject.FindWithTag("Player").GetComponent<BallControl>().release)
        {
            rb.AddForce(-transform.up);
            GameObject.FindWithTag("Player").GetComponent<BallControl>().release = false;
        }
    }
}
