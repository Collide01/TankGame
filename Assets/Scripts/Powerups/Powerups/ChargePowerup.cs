using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChargePowerup : Powerup
{
    public override void Apply(PowerupManager target)
    {
        Pawn targetPawn = target.gameObject.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            targetPawn.specialShotTimer = targetPawn.specialChargeTime;
        }
    }

    public override void Remove(PowerupManager target)
    {
        // Don't
    }
}
