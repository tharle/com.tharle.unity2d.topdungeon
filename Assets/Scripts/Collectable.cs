using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    // Logic 
    protected bool collected;
    
    public string tagCollectable;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag(tagCollectable)) OnCollect();
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
