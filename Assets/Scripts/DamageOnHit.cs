using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "ObstacleCheck")
        {
            // Get the Health component from the Game Object that has the Collider that we are overlapping
            Health otherHealth = other.gameObject.GetComponent<Health>();
            // Only damage if it has a Health component
            if (otherHealth != null)
            {
                // Do damage
                otherHealth.TakeDamage(damageDone, owner);
            }

            // Destroy ourselves, whether we did damage or not
            Destroy(gameObject);
        }
    }
}
