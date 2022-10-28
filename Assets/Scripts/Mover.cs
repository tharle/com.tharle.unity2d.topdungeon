using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    protected BoxCollider2D boxCollider;
    protected float xSpeed = 1f;
    protected float ySpeed = .7f;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        // Reset MoveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, input.z);

        // Swap sprite direction, wether you're going right or left
        if (moveDelta.x > 0) transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        else if (moveDelta.x < 0) transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

        // Add push vector, if any
        moveDelta += pushDirection;

        // Reduce de push force every frame, based of recevery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // Make sure we can move in this direction, by cating a box there first, if box returns null, we're free to move
        var distance = Mathf.Abs(moveDelta.x * Time.deltaTime);
        var direction = new Vector2(moveDelta.x, 0);
        var layersMask = LayerMask.GetMask("Actor", "Blocking");
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, direction, distance, layersMask);
        if (hit.collider == null)
        {
            // Make this thing move!
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

        // Make sure we can move in this direction, by cating a box there first, if box returns null, we're free to move
        distance = Mathf.Abs(moveDelta.y * Time.deltaTime);
        direction = new Vector2(0, moveDelta.y);
        layersMask = LayerMask.GetMask("Actor", "Blocking");
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, direction, distance, layersMask);
        if (hit.collider == null)
        {
            // Make this thing move!
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
    }
}
