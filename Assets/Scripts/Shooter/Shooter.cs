using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void Start();
    public abstract void Update();
    public abstract void Shoot(GameObject shellPrefab, Transform firepointTransform, float fireForce, float damageDone, float lifespan);
    public abstract void BouncyShot(GameObject shellPrefab, Transform firepointTransform, float fireForce);
    public abstract void LaserBeam(GameObject shellPrefab, Transform firepointTransform, float lifespan);
    public abstract void Mine(GameObject shellPrefab, Transform firepointTransform, float lifespan);
}
