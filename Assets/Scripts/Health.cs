using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Set health to max
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount, Pawn source)
    {
        currentHealth = currentHealth - amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            OnDie(source);
        }
    }

    public void Heal(float amount, Pawn source)
    {
        currentHealth = currentHealth + amount;
        Debug.Log(source.name + " healed " + gameObject.name + " by " + amount);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void OnDie(Pawn source)
    {
        // Get the player index if killed by a player
        int playerIndexShooter = GameManager.instance.GetPlayerIndex(source);
        // Get the player index if this object is a player
        int playerIndexThis = GameManager.instance.GetPlayerIndex(gameObject.GetComponent<Pawn>());
        // Award points to that player
        if (playerIndexShooter != -1)
        {
            GameManager.instance.players[playerIndexShooter].score += source.pointsOnKilled;
        }
        // Remove a life if this is a player
        if (playerIndexThis != -1)
        {
            GameManager.instance.players[playerIndexThis].lives--;
            Debug.Log("Decreasing life");
        }
        Destroy(gameObject);
    }
}
