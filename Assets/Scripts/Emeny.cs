using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emeny : Mover
{
    // Experience
    public int xpValue = 1;

    // Logic
    public float triggerLenght = 1;
    public float chaseLenght = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitBox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();

        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // Is player in range?
        float distanceFromPlayer = Vector3.Distance(playerTransform.position, startingPosition);

        if (distanceFromPlayer < chaseLenght)
        {
            if (distanceFromPlayer < triggerLenght) chasing = true;

            if (chasing && !collidingWithPlayer) UpdateMotor((playerTransform.position - transform.position).normalized);

            if (!chasing) UpdateMotor(startingPosition - transform.position);
        } 
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        //Check for overlaps
        VerifyCollideWithPlayer();
    }

    //Same logic from Collidable
    private void VerifyCollideWithPlayer()
    {
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null) continue;

            if (hits[i].tag == "Player") collidingWithPlayer = true;

            // The array is not cleaned up, so we do it ourself;
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        GameManager.instance.ShowText($"+{xpValue} xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1f);
        GameManager.instance.GrantXP(xpValue);
        Destroy(gameObject);
    }
}
