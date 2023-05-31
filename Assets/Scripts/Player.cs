using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover
{

    private SpriteRenderer spriteRenderer;

    // number Per Level.
    private int hpPerLevel = 5;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ReceiveDamage(Damage damage)
    {
        base.ReceiveDamage(damage);
        GameManager.instance.OnHitpointChange();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    internal void OnLevelUp()
    {
        maxHitpoint += hpPerLevel;
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        maxHitpoint = hpPerLevel;
        for (int i = 1; i < level; i++) OnLevelUp();
    }

    public void ToSpawnPoint()
    {
        transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    public void Heal(int healingAmount)
    {
        var strHeal = doHeal(healingAmount);
        GameManager.instance.ShowText(strHeal, 20, Color.green, transform.position, Vector3.up * 30, 1f);
        GameManager.instance.OnHitpointChange();
    }

    private string doHeal(int healingAmount)
    {
        if (hitpoint >= maxHitpoint)
        {
            hitpoint = maxHitpoint;
            return "MAX HP";
        }

        var oldHitpoint = hitpoint;
        hitpoint += healingAmount;

        hitpoint = hitpoint >= maxHitpoint ? maxHitpoint : hitpoint;
        var trueHeal = hitpoint - oldHitpoint;

        return "+" + trueHeal;

    }


}
