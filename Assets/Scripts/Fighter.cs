using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // public fields
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = .2f;

    // Immunity
    protected float immuneTime = 1f; // IFrames
    protected float lastImmune;

    // Push
    protected Vector3 pushDirection;

    // All fighters can ReceiveDamage / Die
    protected virtual void ReceiveDamage(Damage damage)
    {
        if (Time.time - lastImmune <= immuneTime) return;

        lastImmune = Time.time;
        hitpoint -= damage.amount;
        pushDirection = (transform.position - damage.origin).normalized * damage.pushForce;

        GameManager.instance.ShowText(damage.amount.ToString(), 15, Color.red, transform.position, Vector3.zero, .5f);

        if(hitpoint <= 0)
        {
            hitpoint = 0;
            Death();
        }
    }

    protected virtual void Death()
    {

    }
}
