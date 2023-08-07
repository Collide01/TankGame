using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePowerup : Powerup
{
    public int scoreToAdd;
    public override void Apply(PowerupManager target)
    {
        Score targetScore = target.gameObject.GetComponent<Score>();
        if (targetScore != null)
        {
            targetScore.AddScore(scoreToAdd);
        }
    }

    public override void Remove(PowerupManager target)
    {
        // Don't
    }
}
