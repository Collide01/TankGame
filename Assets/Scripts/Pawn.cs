using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Mover mover;
    public Shooter shooter;
    public Health health;

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
}
