using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [HideInInspector] public Mover mover;
    [HideInInspector] public Shooter shooter;
    [HideInInspector] public Health health;

    // Variable for move speed
    public float moveSpeed;
    // Variable for turn speed
    public float turnSpeed;

    public Transform firepointTransform;

    // Variable for our shell prefab
    public GameObject shellPrefab;
    // Variable for our firing force
    public float fireForce;
    // Variable for our damage done
    public float damageDone;
    // Variable for how long our bullets survive if they don't collide
    public float shellLifespan;

    // Variable for Rate of Fire
    [HideInInspector] public float fireRate;
    public float shotsPerSecond;

    public enum SpecialShotType
    {
        BouncyShot,
        LaserBeam,
        Mine
    }
    public SpecialShotType specialShotType;
    // Variable for our special shot prefab
    public GameObject specialShotPrefab;

    // Variable for the special shot's charge time
    public float specialChargeTime; // In seconds
    public Transform specialFirepointTransform;
    public Transform mineTransform;
    // Variable for how long the special shot lasts; only applies to laser beam and mine
    public float specialLifespan;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
        health = GetComponent<Health>();

        fireRate = 1 / shotsPerSecond;
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();
    public abstract void SpecialShoot();
}
