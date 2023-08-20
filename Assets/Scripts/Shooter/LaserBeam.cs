using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float damageDone = 999;
    [HideInInspector] public float lifetime;
    public Pawn owner;
    [SerializeField] private GameObject tankAudioPrefab;

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0 && owner != null)
        {
            owner.firingLaser = false;
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // Get the Health component from the Game Object that has the Collider that we are overlapping
        Health otherHealth = other.gameObject.GetComponent<Health>();
        // Only damage if it has a Health component
        if (otherHealth != null && other.gameObject.GetComponent<Pawn>() != owner)
        {
            // Do damage
            otherHealth.TakeDamage(damageDone, owner);

            GameObject tankAudio = Instantiate(tankAudioPrefab, transform.position, Quaternion.identity);
            tankAudio.GetComponent<GameAudioSource>().PlayAudio(0);
        }
    }
}
