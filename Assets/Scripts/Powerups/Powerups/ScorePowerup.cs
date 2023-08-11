using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScorePowerup : Powerup
{
    public int scoreToAdd;
    public override void Apply(PowerupManager target)
    {
        Pawn targetPawn = target.gameObject.GetComponent<Pawn>();
        int playerIndexThis = GameManager.instance.GetPlayerIndex(targetPawn);
        if (playerIndexThis != -1)
        {
            GameManager.instance.players[playerIndexThis].score += scoreToAdd;
        }
    }

    public override void Remove(PowerupManager target)
    {
        // Don't
    }
}
