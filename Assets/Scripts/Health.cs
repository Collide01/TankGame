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
        Destroy(gameObject);
    }
}
