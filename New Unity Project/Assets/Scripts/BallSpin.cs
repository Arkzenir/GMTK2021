
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
    private GameObject player;
    private BallControl playerControlScript;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        if(target != null) 
        {
            relativeDistance = transform.position - target.position;
        }

        player = GameObject.FindWithTag("Player");
        playerControlScript = player.GetComponent<BallControl>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerControlScript.spin)
        {
            axis = new Vector3(0, 0, 1);
            rb.velocity = Vector2.zero;
            if (target != null)
            {
                transform.position = (target.position + relativeDistance);
                transform.RotateAround(target.position, axis, orbitDegreesPerSec * Time.deltaTime);
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

        if (playerControlScript.release)
        {
            rb.AddForce(-transform.up);
            playerControlScript.release = false;
        }
    }
}
