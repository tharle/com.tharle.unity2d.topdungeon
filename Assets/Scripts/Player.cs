using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() 
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Reset MoveDelta
        moveDelta = new Vector3(x, y, 0);

        // Swap sprite direction, wether you're going right or left
        if(moveDelta.x > 0) transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        else if(moveDelta.x < 0) transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        
        // Make sure we can move in this direction, by cating a box there first, if box returns null, we're free to move
        var distance = Mathf.Abs(moveDelta.x * Time.deltaTime);
        var direction = new Vector2(moveDelta.x, 0);
        var layersMask = LayerMask.GetMask("Actor", "Blocking");
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, direction, distance, layersMask);
        if(hit.collider == null)
        {
            // Make this thing move!
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

        // Make sure we can move in this direction, by cating a box there first, if box returns null, we're free to move
        distance = Mathf.Abs(moveDelta.y * Time.deltaTime);
        direction = new Vector2(0, moveDelta.y);
        layersMask = LayerMask.GetMask("Actor", "Blocking");
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, direction, distance, layersMask);
        if(hit.collider == null)
        {
            // Make this thing move!
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        
        
    }
}
