using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    private Vector2 vInput;
    private float fireValue;
    private float specialFireValue;

    // Start is called before the first frame update
    public override void Start()
    {
        // Run the Start() function from the parent (base) class
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Process out inputs
        if (vInput.y > 0) pawn.MoveForward();
        if (vInput.y < 0) pawn.MoveBackward();
        if (vInput.x > 0) pawn.RotateClockwise();
        if (vInput.x < 0) pawn.RotateCounterClockwise();

        // Run the Update() function from the parent (base) class
        base.Update();
    }

    private void OnMove(InputValue value)
    {
        vInput = value.Get<Vector2>();
    }

    private void OnFire(InputValue value)
    {
        fireValue = value.Get<float>();
    }

    private void OnSpecialFire(InputValue value)
    {
        specialFireValue = value.Get<float>();
    }

    private void OnPause(InputValue value)
    {
        float v = value.Get<float>();
        if (v != 0)
        {
            Debug.Log("Paused");
        }
    }
}