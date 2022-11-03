using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int[] damagePoints = { 1,    2,      3,      4,      5,      6,      7};
    public float[] pushForces = { 2f,   2.2f,   2.5f,   3f,     3.2f,   3.6f,   4f};

    // Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    // Swing
    private Animator animator;
    private float coolDown = .5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
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
                amount = damagePoints[weaponLevel],
                origin = transform.position,
                pushForce = pushForces[weaponLevel]
            };
            // then we'll send it the fighter we've hit
            coll.SendMessage(Damage.MSG_RECEIVE_DAMAGE, damage);
        }
    }

    private void Swing()
    {
        animator.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        OnChangeWeapon();
    }

    private void OnChangeWeapon() {
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
        //Change stats
    }

    public void SetWeapon(int weaponLevelLoad)
    {
        weaponLevel = weaponLevelLoad;
        OnChangeWeapon();
    }
}
