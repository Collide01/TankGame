using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private float shootTimer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        shootTimer += Time.deltaTime;

        base.Start();
    }

    // Calls Mover to move the tank forward
    public override void MoveForward()
    {
        if (mover != null)
        {
            mover.Move(transform.forward, moveSpeed);
        }
        else
        {
            // Failsafe
            Debug.LogWarning("Warning: No Mover in TankPawn.MoveForward()!");
        }
    }

    // Calls Mover to move the tank backward
    public override void MoveBackward()
    {
        if (mover != null)
        {
            mover.Move(transform.forward, -moveSpeed);
        }
        else
        {
            // Failsafe
            Debug.LogWarning("Warning: No Mover in TankPawn.MoveBackward()!");
        }
    }

    // Calls Mover to rotate the tank clockwise
    public override void RotateClockwise()
    {
        if (mover != null)
        {
            mover.Rotate(turnSpeed);
        }
        else
        {
            // Failsafe
            Debug.LogWarning("Warning: No Mover in TankPawn.RotateClockwise()!");
        }
    }

    // Calls Mover to rotate the tank counterclockwise
    public override void RotateCounterClockwise()
    {
        if (mover != null)
        {
            mover.Rotate(-turnSpeed);
        }
        else
        {
            // Failsafe
            Debug.LogWarning("Warning: No Mover in TankPawn.RotateCounterClockwise()!");
        }
    }

    public override void Shoot()
    {
        if (shootTimer >= fireRate)
        {
            shooter.Shoot(shellPrefab, firepointTransform, fireForce, damageDone, shellLifespan);
            shootTimer = 0;
        }
    }
}
