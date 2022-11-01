using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int damagePoint = 1;
    public float pushForce = 2f;

    // Upgrade
    public int weaponLivel = 0;
    private SpriteRenderer spriteRenderer;

    // Swing
    private Animator animator;
    private float coolDown = .5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > coolDown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.CompareTag("Fighter"))
        {
            // Create new damage object
            Damage damage = new Damage()
            {
                amount = damagePoint,
                origin = transform.position,
                pushForce = pushForce
            };
            // then we'll send it the fighter we've hit
            coll.SendMessage(Damage.MSG_RECEIVE_DAMAGE, damage);
        }
    }

    private void Swing()
    {
        animator.SetTrigger("Swing");
    }
}
