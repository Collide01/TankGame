using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PlayerController : Controller
{
    private Vector2 vInput;
    private bool firing;
    private bool specialFiring;

    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text livesText;
    public Slider healthSlider;
    public Slider specialShotSlider;

    public int score;
    public int lives = 3;

    // Start is called before the first frame update
    public override void Start()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it tracks the player(s)
            if (GameManager.instance.players != null)
            {
                // Register with the GameManager
                GameManager.instance.players.Add(this);
            }
        }

        transform.position = pawn.transform.position;
        pawn.gameObject.layer = 7; // Player layer
        pawn.health.maxHealth = 10;
        pawn.health.currentHealth = 10;
        pawn.ownedByPlayer = true;

        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (GameManager.instance != null)
        {
            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + GameManager.instance.highScore;
            }
        }

        if (healthSlider != null)
        {
            healthSlider.maxValue = pawn.health.maxHealth;
            healthSlider.value = pawn.health.maxHealth;
        }

        if (specialShotSlider != null)
        {
            specialShotSlider.maxValue = pawn.specialChargeTime;
        }

        // Run the Start() function from the parent (base) class
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (pawn != null)
        {
            transform.position = pawn.transform.position;
            transform.rotation = pawn.transform.rotation;

            // Process out inputs
            if (vInput.y > 0) pawn.MoveForward();
            if (vInput.y < 0) pawn.MoveBackward();
            if (vInput.x > 0) pawn.Rotate(pawn.turnSpeed);
            if (vInput.x < 0) pawn.Rotate(-pawn.turnSpeed);
            if (vInput.x == 0 && vInput.y == 0) pawn.StayStill();

            if (firing) pawn.Shoot();
            if (specialFiring) pawn.SpecialShoot();

            if (healthSlider != null)
            {
                healthSlider.maxValue = pawn.health.maxHealth;
                healthSlider.value = pawn.health.currentHealth;
            }

            if (specialShotSlider != null)
            {
                specialShotSlider.value = pawn.specialShotTimer;
            }
        }

        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (GameManager.instance != null)
        {
            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + GameManager.instance.highScore;
            }
        }

        // Run the Update() function from the parent (base) class
        base.Update();
    }

    private void OnMove(InputValue value)
    {
        vInput = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        float v = value.Get<float>();
        if (v != 0)
        {
            firing = true;
        }
        else
        {
            firing = false;
        }
    }

    private void OnSpecialFire(InputValue value)
    {
        float v = value.Get<float>();
        if (v != 0)
        {
            specialFiring = true;
        }
        else
        {
            specialFiring = false;
        }
    }

    private void OnPause(InputValue value)
    {
        float v = value.Get<float>();
        if (v != 0)
        {
            Debug.Log("Paused");
        }
    }

    public void OnDestroy()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it tracks the player(s)
            if (GameManager.instance.players != null)
            {
                // Deregister with the GameManager
                GameManager.instance.players.Remove(this);
            }
        }
    }
}
