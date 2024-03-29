using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collidable : MonoBehaviour
{
    public ContactFilter2D Filter;
    private BoxCollider2D boxCollider;
    private BoxCollider2D[] hits = new BoxCollider2D[10];

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        // Collision work
        boxCollider.OverlapCollider(Filter, hits);
        
        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i] == null) continue;

            OnCollide(hits[i]);

            // The array is not cleaned up, so we do it ourself;
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("OnCollide was not implemented in : "+ this.name);
    }
}
