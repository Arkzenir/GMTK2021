using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLine : MonoBehaviour
{
    public Transform ConnectTo;
    public bool UpdateChain = true;
    public float spriteScale = 1f;

    private BallControl _control;
    private LineRenderer _renderer;

    private void Start()
    {
        _control = GetComponent<BallControl>();
        _renderer = GetComponent<LineRenderer>();

        ConnectTo = GameObject.FindWithTag("Ball").transform;
    }

    private void LateUpdate()
    {
        
        if (_renderer != null)
        {
            if (ConnectTo != null && _control.spin)
            {
                if (UpdateChain)
                {
                    int spriteCount = Mathf.FloorToInt(Vector3.Distance(ConnectTo.position, transform.position) / spriteScale);
 
                    Vector3[] positions = {
                        transform.position,
                        (ConnectTo.position - transform.position).normalized * spriteScale * spriteCount
                    };
                    
                    
                    _renderer.positionCount = positions.Length;
                    _renderer.SetPositions(positions);
 
                    if (_renderer.material != null)
                        _renderer.material.mainTextureScale = new Vector2(spriteScale * spriteCount, 1);
                    else
                        Debug.LogError(name + "'s Line Renderer has no material!");
                }
            }
            else
            {
                _renderer.positionCount = 0;
            }
        }
        else
        {
            Debug.Log(name + " has no LineRenderer component!");
        }
    }
}