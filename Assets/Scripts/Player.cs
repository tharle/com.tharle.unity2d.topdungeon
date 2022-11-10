using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover
{

    private SpriteRenderer spriteRenderer;

    // number Per Level
    private int hpPerLevel = 5;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(gameObject);
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
}
