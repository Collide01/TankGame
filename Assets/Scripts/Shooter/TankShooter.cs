using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    // Start is called before the first frame update
    public override void Start()
    {
    }

    // Update is called once per frame
    public override void Update()
    {
    }

    public override void Shoot(GameObject shellPrefab, Transform firepointTransform, float fireForce, float damageDone, float lifespan)
    {
        // Instantiate our projectile
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;
        // Get the DamageOnHit component
        DamageOnHit doh = newShell.GetComponent<DamageOnHit>();
        // If it has one... 
        if (doh != null)
        {
            // ... set the damageDone in the DamageOnHit component to the value passed in
            doh.damageDone = damageDone;
            // ... set the owner to the pawn that shot this shell, if there is one (otherwise, owner is null).
            doh.owner = GetComponent<Pawn>();
        }
        // Get the rigidbody component
        Rigidbody rb = newShell.GetComponent<Rigidbody>();
        // If it has one
        if (rb != null)
        {
            // ... AddForce to make it move forward
            rb.AddForce(firepointTransform.forward * fireForce);
        }
        // Destroy it after a set time
        Destroy(newShell, lifespan);
    }

    public override void BouncyShot(GameObject shellPrefab, Transform firepointTransform, float fireForce)
    {
        // Instantiate our projectile
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;
        // Get the DamageOnHit component
        BouncyShot bouncy = newShell.GetComponent<BouncyShot>();
        // If it has one... 
        if (bouncy != null)
        {
            // ... set the owner to the pawn that shot this shell, if there is one (otherwise, owner is null).
            bouncy.owner = GetComponent<Pawn>();
        }
        // Get the rigidbody component
        Rigidbody rb = newShell.GetComponent<Rigidbody>();
        // If it has one
        if (rb != null)
        {
            // ... AddForce to make it move forward
            rb.AddForce(firepointTransform.forward * fireForce);
        }
    }

    public override void LaserBeam(GameObject shellPrefab, Transform firepointTransform, float lifespan)
    {
        throw new System.NotImplementedException();
    }

    public override void Mine(GameObject shellPrefab, Transform firepointTransform, float lifespan)
    {
        throw new System.NotImplementedException();
    }
}
