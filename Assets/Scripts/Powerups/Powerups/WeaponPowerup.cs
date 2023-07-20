using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponPowerup : Powerup
{
    public override void Apply(PowerupManager target)
    {
        TankPawn targetPawn = target.gameObject.GetComponent<TankPawn>();
        if (targetPawn != null)
        {
            targetPawn.ChangeWeapon();
        }
    }

    public override void Remove(PowerupManager target)
    {
        // Don't
    }
}
