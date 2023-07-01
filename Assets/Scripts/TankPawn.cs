using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private float shootTimer;
    private float specialShotTimer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        shootTimer += Time.deltaTime;
        specialShotTimer += Time.deltaTime;
        specialShotTimer = Mathf.Clamp(specialShotTimer, 0, specialChargeTime);

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
    public override void Rotate(float setTurnSpeed)
    {
        if (mover != null)
        {
            mover.Rotate(setTurnSpeed);
        }
        else
        {
            // Failsafe
            Debug.LogWarning("Warning: No Mover in TankPawn.Rotate()!");
        }
    }

    public override void Shoot()
    {
        if (shootTimer >= fireRate)
        {
            shooter.Shoot(shellPrefab, firepointTransform, fireForce, damageDone, shellLifespan);
            shootTimer = 0;
            specialShotTimer -= 2;
        }
    }

    public override void SpecialShoot()
    {
        if (specialShotTimer >= specialChargeTime)
        {
            switch (specialShotType)
            {
                case SpecialShotType.BouncyShot:
                    shooter.BouncyShot(specialShotPrefab, specialFirepointTransform, fireForce);
                    break;
                case SpecialShotType.LaserBeam:
                    shooter.LaserBeam(specialShotPrefab, specialFirepointTransform, specialLifespan);
                    break;
                case SpecialShotType.Mine:
                    shooter.Mine(specialShotPrefab, specialFirepointTransform, specialLifespan);
                    break;
            }
            specialShotTimer = 0;
        }
    }

    public override void RotateTowards(Vector3 targetPosition, float avoidanceSpeed = 0)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        if (avoidanceSpeed != 0)
        {
            if (avoidanceSpeed > 0) mover.Rotate(turnSpeed + avoidanceSpeed);
            else mover.Rotate(-turnSpeed + avoidanceSpeed);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
}
