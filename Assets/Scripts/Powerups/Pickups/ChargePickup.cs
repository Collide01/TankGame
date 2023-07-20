using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePickup : MonoBehaviour
{
    public ChargePowerup chargePowerup;

    private void OnTriggerEnter(Collider other)
    {
        PowerupManager manager = other.gameObject.GetComponent<PowerupManager>();
        if (manager)
        {
            manager.Add(chargePowerup);
            Destroy(gameObject);
        }
    }
}
