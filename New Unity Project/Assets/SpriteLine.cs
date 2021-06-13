using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLine : MonoBehaviour
{
    public List<Transform> ConnectTo;
    public bool UpdateChain = true;
    public float spriteScale = 1f;
 
    void Update()
    {
        LineRenderer renderer = GetComponent<LineRenderer>();
        if (renderer != null)
        {
            if (ConnectTo.Count != 0)
            {
                if (UpdateChain)
                {
                    foreach (var transform in ConnectTo)
                    {
                        int spriteCount = Mathf.FloorToInt(Vector3.Distance(transform.position, transform.position) / spriteScale);
 
                        Vector3[] positions = new Vector3[] {
                            transform.position,
                            (transform.position - transform.position).normalized * spriteScale * spriteCount
                        };
 
                        renderer.positionCount = positions.Length;
                        renderer.SetPositions(positions);
 
                        if (renderer.material != null)
                            renderer.material.mainTextureScale = new Vector2(spriteScale * spriteCount, 1);
                        else
                            Debug.LogError(name + "'s Line Renderer has no material!");
                    }
                }
            }
            else
            {
                renderer.positionCount = 0;
            }
        }
        else
        {
            Debug.Log(name + " has no LineRenderer component!");
        }
    }

    public bool AddBall(Transform t)
    {
        if (t == null) return false;
        ConnectTo.Add(t);
        return true;
    }
    
    public bool RemoveBall(Transform t)
    {
        if (t == null || !ConnectTo.Contains(t)) return false;
        ConnectTo.Remove(t);
        return true;
    }
}