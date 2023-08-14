using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyShot : MonoBehaviour
{
    public float damageDone = 999;
    public int bounces; // Bounces a certain number of times before it gets destroyed
    public Pawn owner;
    [SerializeField] private GameObject tankAudioPrefab;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "ObstacleCheck")
        {
            // Get the Health component from the Game Object that has the Collider that we are overlapping
            Health otherHealth = collision.collider.gameObject.GetComponent<Health>();
            // Only damage if it has a Health component
            if (otherHealth != null)
            {
                // Do damage
                otherHealth.TakeDamage(damageDone, owner);

                GameObject tankAudio = Instantiate(tankAudioPrefab, transform.position, Quaternion.identity);
                tankAudio.GetComponent<GameAudioSource>().PlayAudio(0);

                Destroy(gameObject);
            }

            bounces--;
            if (bounces <= 0) Destroy(gameObject);
        }
    }
}
