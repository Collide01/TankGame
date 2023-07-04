using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterData : MonoBehaviour
{
    public float bulletSpeed;
    public float fireRate;
    public Pawn.SpecialShotType specialShot;

    private void Start()
    {
        if (GetComponentInParent<Pawn>() != null)
        {
            GetComponentInParent<Pawn>().fireForce = bulletSpeed;
            GetComponentInParent<Pawn>().shotsPerSecond = fireRate;
            GetComponentInParent<Pawn>().specialShotType = specialShot;
        }
    }
}
