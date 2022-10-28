using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    // Damage 
    public int damageAmount;
    public float pushForce;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player")) {
            // Create a new damage object
            Damage damage = new Damage() { 
                amount = damageAmount,
                pushForce = pushForce,
                origin = transform.position
            };

            // Sending it to the player
            GameManager.instance.player.SendMessage(Damage.MSG_RECEIVE_DAMAGE, damage);
        }
    }
}
