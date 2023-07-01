using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerController : Controller
{
    private Vector2 vInput;
    private bool firing;
    private bool specialFiring;

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
        // Run the Start() function from the parent (base) class
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Process out inputs
        if (vInput.y > 0) pawn.MoveForward();
        if (vInput.y < 0) pawn.MoveBackward();
        if (vInput.x > 0) pawn.Rotate(pawn.turnSpeed);
        if (vInput.x < 0) pawn.Rotate(-pawn.turnSpeed);

        if (firing) pawn.Shoot();
        if (specialFiring) pawn.SpecialShoot();

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

            // And it tracks the controller(s)
            if (GameManager.instance.controllers != null)
            {
                // Deregister with the GameManager
                GameManager.instance.controllers.Remove(this);
            }
        }
    }
}
