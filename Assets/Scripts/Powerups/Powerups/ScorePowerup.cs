using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePowerup : Powerup
{
    public float scoreToAdd;
    public override void Apply(PowerupManager target)
    {
        Health targetHealth = target.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.Heal(scoreToAdd, target.gameObject.GetComponent<Pawn>());
        }
    }

    public override void Remove(PowerupManager target)
    {
        // Don't
    }
}
