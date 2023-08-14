using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponPowerup weaponPowerup;
    [SerializeField] private GameObject tankAudioPrefab;

    private void OnTriggerEnter(Collider other)
    {
        PowerupManager manager = other.gameObject.GetComponent<PowerupManager>();
        if (manager)
        {
            manager.Add(weaponPowerup);

            GameObject tankAudio = Instantiate(tankAudioPrefab, transform.position, Quaternion.identity);
            tankAudio.GetComponent<GameAudioSource>().PlayAudio(1);

            Destroy(gameObject);
        }
    }
}
