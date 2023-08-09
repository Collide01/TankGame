using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    public ScorePowerup scorePowerup;

    private void OnTriggerEnter(Collider other)
    {
        PowerupManager manager = other.gameObject.GetComponent<PowerupManager>();
        if (manager)
        {
            manager.Add(scorePowerup);
            Destroy(gameObject);
        }
    }
}
